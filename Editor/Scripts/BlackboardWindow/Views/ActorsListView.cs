using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ActorsListView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ActorsListView, UxmlTraits> { }

    private readonly string uxmlPath = "UXML/ActorsListView.uxml";

    //public Action<int, BlackboardEventType> onEventTypeChanged; 
    
    private MultiColumnListView _listView;
    private ActorGroupSO _actorGroup;
    private SerializedProperty _eventsProperty;
    private List<ActorSO> _actors => _actorGroup.elementsList;

    public ActorSO[] actorsSelected => _listView.selectedItems.Cast<ActorSO>().ToArray();
    
    public ActorsListView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);
        
        _listView = this.Q<MultiColumnListView>();
        
        Setup();
    }

    private void Setup()
    {
        _listView.columns["name"].makeCell = MakeNameCell;
        _listView.columns["description"].makeCell = MakeDescriptionCell;
        _listView.columns["prefab"].makeCell = MakePrefabCell;
        
        _listView.columns["name"].bindCell = (element, i) => BindName(element, _actors[i]);
        _listView.columns["description"].bindCell = (element, i) => BindDescription(element, new SerializedObject(_actors[i]));
        _listView.columns["prefab"].bindCell = (element, i) => BindPrefab(element, new SerializedObject(_actors[i]));
    }
    
    public void Populate(ActorGroupSO actorGroup)
    {
        _actorGroup = actorGroup;
        _listView.itemsSource = _actors;
        _listView.RefreshItems();
    }

    #region Modify list
    public void Add(ActorSO actor)
    {
        _actorGroup.AddElement(actor);
        _listView.RefreshItems();

        int lastIndex = _actorGroup.elementsList.Count - 1;
        
        _listView.SetSelection(lastIndex);
        var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
        var itemView = list.ElementAt(lastIndex);
        var nameField = itemView.Q<TextField>("name-field");
        nameField.Focus();
    }
    
    public void Remove(params ActorSO[] actors)
    {
        foreach (ActorSO actor in actors)
            _actorGroup.RemoveElement(actor.id);    
        
        _listView.RefreshItems();
    }
    #endregion

    #region Make
    private VisualElement MakeCell()
    {
        var cell = new VisualElement();
        cell.AddToClassList("centered");
        cell.style.paddingTop = 5f;
        cell.style.paddingBottom = 5f;
        
        return cell;
    }
    
    private VisualElement MakeNameCell()
    {
        var cell = MakeCell();
        
        var nameField = new TextField("");
        nameField.name = "name-field";
        nameField.bindingPath = "theName";
        cell.Add(nameField);
        
        return cell;
    }
    
    private VisualElement MakeDescriptionCell()
    {
        var cell = MakeCell();
        
        var descriptionField = new TextField("");
        descriptionField.name = "description-field";
        descriptionField.bindingPath = "description";
        
        cell.Add(descriptionField);

        return cell;
    }
    
    private VisualElement MakePrefabCell()
    {
        var cell = MakeCell();
        
        var prefabField = new ObjectField();
        prefabField.objectType = typeof(GameObject);
        prefabField.bindingPath = "prefab";
        
        cell.Add(prefabField);

        return cell;
    }
    #endregion
    
    #region Bind
    private void BindName(VisualElement cell, ActorSO actorSo)
    {
        TextField nameField = cell.Q<TextField>();
        nameField.value = actorSo.theName;
        
        nameField.RegisterCallback<FocusOutEvent>(e =>
        {
            bool isValid = BlackboardValidator.ValidateElementName(nameField.value, actorSo.theName);
            
            if(isValid)
                actorSo.SetName(nameField.value);
            else
                nameField.value = actorSo.theName;
        });
    }
    
    private void BindDescription(VisualElement cell, SerializedObject serializedObject)
    {
        TextField descriptionField = cell.Q<TextField>();
        descriptionField.Bind(serializedObject);
    }
    
    private void BindPrefab(VisualElement cell, SerializedObject serializedObject)
    {
        ObjectField prefabField = cell.Q<ObjectField>();
        prefabField.Bind(serializedObject);
    }
    #endregion
}
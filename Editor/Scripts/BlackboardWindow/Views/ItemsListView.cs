using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemsListView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemsListView, UxmlTraits> { }

    private readonly string uxmlPath = "UXML/ItemsListView.uxml";

    //public Action<int, BlackboardEventType> onEventTypeChanged; 
    
    private MultiColumnListView _listView;
    private ItemGroupSO _itemGroup;
    private SerializedProperty _eventsProperty;
    private List<ItemSO> _items => _itemGroup.elementsList;

    public ItemSO[] itemsSelected => _listView.selectedItems.Cast<ItemSO>().ToArray();
    
    public ItemsListView()
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
        
        _listView.columns["name"].bindCell = (element, i) => BindName(element, _items[i]);
        _listView.columns["description"].bindCell = (element, i) => BindDescription(element, new SerializedObject(_items[i]));
        _listView.columns["prefab"].bindCell = (element, i) => BindPrefab(element, new SerializedObject(_items[i]));
    }
    
    public void Populate(ItemGroupSO itemGroup)
    {
        _itemGroup = itemGroup;
        _listView.itemsSource = _items;
        _listView.RefreshItems();
    }

    #region Modify list
    public void Add(ItemSO item)
    {
        _itemGroup.AddElement(item);
        _listView.RefreshItems();
        
        int lastIndex = _itemGroup.elementsList.Count - 1;
        
        _listView.SetSelection(lastIndex);
        var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
        var itemView = list.ElementAt(lastIndex);
        var nameField = itemView.Q<TextField>("name-field");
        nameField.Focus();
    }
    
    public void Remove(params ItemSO[] items)
    {
        foreach (ItemSO item in items)
            _itemGroup.RemoveElement(item.id);    
        
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
    private void BindName(VisualElement cell, ItemSO itemSo)
    {
        TextField nameField = cell.Q<TextField>();
        nameField.value = itemSo.theName;

        nameField.RegisterCallback<FocusOutEvent>(e =>
        {
            bool isValid = BlackboardValidator.ValidateElementName(nameField.value, itemSo.theName);
            
            if(isValid)
                itemSo.SetName(nameField.value);
            else
                nameField.value = itemSo.theName;
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
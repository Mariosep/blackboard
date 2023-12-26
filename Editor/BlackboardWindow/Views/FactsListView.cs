using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

using UnityEngine.UIElements;

public class FactsListView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<FactsListView, UxmlTraits> { }

    private readonly string uxmlPath = "UXML/FactsListView.uxml";

    public Action<int, FactType> onFactTypeChanged; 
    
    private MultiColumnListView _listView;
    private FactGroupSO _factGroup;
    private SerializedProperty _factsProperty;
    private List<FactSO> _facts => _factGroup.elementsList;

    public FactSO[] factsSelected => _listView.selectedItems.Cast<FactSO>().ToArray();
    
    public FactsListView()
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
        _listView.columns["value"].makeCell = MakeCell;
        _listView.columns["description"].makeCell = MakeDescriptionCell;
        _listView.columns["type"].makeCell = MakeTypeCell;
        
        _listView.columns["name"].bindCell = (element, i) => BindName(element, _facts[i]);
        _listView.columns["value"].bindCell = (element, i) => BindValue(element, _facts[i]);
        _listView.columns["description"].bindCell = (element, i) => BindDescription(element, _facts[i]);
        _listView.columns["type"].bindCell = BindType;
    }
    
    public void Populate(FactGroupSO factGroup)
    {
        _factGroup = factGroup;
        _listView.itemsSource = _facts;
        _listView.RefreshItems();
    }

    #region Modify list
    public void Add(FactSO fact)
    {
        string validName = BlackboardValidator.GetValidName(_factGroup, fact.theName, fact, true);
        fact.SetName(validName);
        
        _factGroup.AddElement(fact);
        _listView.RefreshItems();
        
        int lastIndex = _factGroup.elementsList.Count - 1;
        
        _listView.SetSelection(lastIndex);
        var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
        var itemView = list.ElementAt(lastIndex);
        var nameField = itemView.Q<TextField>("name-field");
        nameField.Focus();
    }
    
    public void Remove(params FactSO[] facts)
    {
        foreach (FactSO fact in facts)
            _factGroup.RemoveElement(fact.id);    
        
        _listView.RefreshItems();
    }

    public void Replace(int i, FactSO fact)
    {
        _factGroup.Replace(i, fact);
        _listView.RefreshItem(i);
    }
    #endregion

    #region Make
    private VisualElement MakeCell()
    {
        var cell = new VisualElement();
        cell.AddToClassList("centered-vertical");
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
    
    private VisualElement MakeTypeCell()
    {
        var cell = MakeCell();
        
        var enumField = new EnumField(FactType.Bool);
        enumField.bindingPath = "type";
        cell.Add(enumField);

        return cell;
    }
    #endregion
    
    #region Bind
    private void BindName(VisualElement cell, FactSO fact)
    {
        cell.RemoveAt(0);
        var nameField = new TextField("")
        {
            name = "name-field",
            value = fact.theName
        };
        nameField.RegisterCallback<FocusOutEvent>(e => ValidateAndSetName(nameField, fact));
        
        cell.Add(nameField);
    }
    
    private void BindValue(VisualElement cell, FactSO fact)
    {
        if(cell.childCount > 0)
            cell.RemoveAt(0);
        
        VisualElement content = FactValueViewFactory.CreateValueView(fact);
        cell.Add(content);
    }
    
    private void BindDescription(VisualElement cell, FactSO fact)
    {
        TextField descriptionField = cell.Q<TextField>();
        descriptionField.value = fact.description;
        
        descriptionField.RegisterCallback<FocusOutEvent>(e =>
        {
            descriptionField.value = descriptionField.value.Trim();
            fact.description = descriptionField.value;
        });
    }
    
    private void BindType(VisualElement cell, int i)
    {
        EnumField typeField = cell.Q<EnumField>();
        
        typeField.value = _facts[i].type;
        typeField.RegisterValueChangedCallback(e => onFactTypeChanged?.Invoke(i, (FactType)e.newValue));
    }
    #endregion
    
    private void ValidateAndSetName(TextField nameField, FactSO fact)
    {
        string validName = BlackboardValidator.GetValidName(_factGroup, nameField.value, fact); 
        fact.SetName(validName);
            
        nameField.value = fact.theName;
    }
}
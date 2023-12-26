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

    private MultiColumnListView _listView;
    private ItemGroupSO _itemGroup;
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
        _listView.columns["description"].bindCell = (element, i) => BindDescription(element, _items[i]);
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
        string validName = BlackboardValidator.GetValidName(_itemGroup, item.theName, item, true);
        item.SetName(validName);
        
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
    private void BindName(VisualElement cell, ItemSO item)
    {
        cell.RemoveAt(0);
        var nameField = new TextField("")
        {
            name = "name-field",
            value = item.theName
        };
        nameField.RegisterCallback<FocusOutEvent>(e => ValidateAndSetName(nameField, item));
        
        cell.Add(nameField);
    }
    
    private void BindDescription(VisualElement cell, ItemSO item)
    {
        TextField descriptionField = cell.Q<TextField>();
        descriptionField.value = item.description;
        
        descriptionField.RegisterCallback<FocusOutEvent>(e =>
        {
            descriptionField.value = descriptionField.value.Trim();
            item.description = descriptionField.value;
        });
    }
    
    private void BindPrefab(VisualElement cell, SerializedObject serializedObject)
    {
        ObjectField prefabField = cell.Q<ObjectField>();
        prefabField.Bind(serializedObject);
    }
    #endregion
    
    private void ValidateAndSetName(TextField nameField, ItemSO item)
    {
        string validName = BlackboardValidator.GetValidName(_itemGroup, nameField.value, item); 
        item.SetName(validName);
            
        nameField.value = item.theName;
    }
}
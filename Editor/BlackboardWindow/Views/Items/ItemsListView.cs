using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blackboard.Items;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Items
{
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
            string validName = BlackboardValidator.GetValidName(_itemGroup, "", item.Name, item, true);
            item.Name = validName;
        
            _itemGroup.AddElement(item);
            _listView.RefreshItems();
        
            int lastIndex = _itemGroup.elementsList.Count - 1;
        
            _listView.SetSelection(lastIndex);
            var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
            var itemView = list.ElementAt(lastIndex);
            var nameField = itemView.Q<TextField>("theName");
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
        private VisualElement MakeNameCell() => new TextFieldItem(nameof(ItemSO.theName));
        private VisualElement MakeDescriptionCell() => new TextFieldItem(nameof(ItemSO.description));
        private VisualElement MakePrefabCell()
        {
            var prefabField = new ObjectField();
            prefabField.objectType = typeof(GameObject);
            prefabField.bindingPath = "prefab";
            return prefabField;
        }
        #endregion
    
        #region Bind
        private void BindName(VisualElement cell, ItemSO item)
        {
            var nameField = cell as TextFieldItem;
            nameField.SetDataSource(item);
            nameField.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetName(e.previousValue, e.newValue, item));
        }
    
        private void BindDescription(VisualElement cell, ItemSO item)
        {
            var descriptionField = cell as TextFieldItem;
            descriptionField.SetDataSource(item);
            descriptionField.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetDescription(e.previousValue, e.newValue, item));
        }
    
        private void BindPrefab(VisualElement cell, SerializedObject serializedObject)
        {
            ObjectField prefabField = cell.Q<ObjectField>();
            prefabField.Bind(serializedObject);
        }
        #endregion
    }
}
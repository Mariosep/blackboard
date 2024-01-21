using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class GroupListView : VisualElement
    {
        private const string UxmlPath = "UXML/GroupListView.uxml";

        public Action<int> onGroupSelected;

        private IDataBase _dataBase;
    
        protected ListView _listView;
    
        public GroupListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            _listView = this.Q<ListView>();
            _listView.selectionChanged += OnGroupSelected;
        
            Add(_listView);
        
            Setup();
        }

        protected virtual void Setup()
        {
            _listView.makeItem = MakeItem;
            _listView.bindItem = (element, i) => BindItem(element, new SerializedObject(_dataBase.GroupList[i]));
        }

        public void PopulateView(IDataBase dataBase)
        {
            _dataBase = dataBase;
        
            if(_dataBase.GroupListLength > 0)
            {
                _listView.itemsSource = _dataBase.GroupList;
            
                SetSelection(0);
            }
        }

        protected VisualElement MakeItem()
        {
            var label = new Label();
            label.bindingPath = "groupName";
            label.AddToClassList("item-list");
        
            return label;
        }
    
        protected void BindItem(VisualElement element, SerializedObject so)
        {
            Label label = element as Label;
            label.Bind(so);
        }
    
        public void AddGroup(ScriptableObject group)
        {
            _dataBase.AddGroup(group);
        
            _listView.itemsSource = _dataBase.GroupList;
            _listView.RefreshItems();
        
            if(_dataBase.GroupListLength == 1)
            {
                _listView.visible = true;
            
                SetSelection(0);
            }
        
            _listView.RefreshItems();
        }
    
        public void RemoveGroup(string id)
        {
            _dataBase.RemoveGroup(id);
        
            _listView.itemsSource = _dataBase.GroupList;
            _listView.RefreshItems();

            if(_dataBase.GroupListLength == 0)
            {
                _listView.visible = false;
            }
            else
                SetSelection(0);    
        }

        protected void SetSelection(int index)
        {
            if(_listView.itemsSource != null && _listView.itemsSource.Count > index)
                _listView.SetSelection(index);
        }
    
        private void OnGroupSelected(IEnumerable<object> itemsSelected)
        {
            int groupIndex = _listView.selectedIndex;
            onGroupSelected?.Invoke(groupIndex);
        }
    }
}
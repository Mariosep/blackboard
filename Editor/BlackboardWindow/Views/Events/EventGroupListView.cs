using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class EventGroupListView : VisualElement
    {
        private const string UxmlPath = "UXML/GroupListView.uxml";

        public Action<int> onGroupSelected;

        // Data
        private List<string> eventCategories;

        // Visual elements
        private ListView _listView;

        public EventGroupListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            _listView = this.Q<ListView>();
            _listView.selectionChanged += OnGroupSelected;

            Setup();
        }

        private void Setup()
        {
            _listView.makeItem = MakeItem;
            _listView.bindItem = (element, i) => BindItem(element, eventCategories[i]);
        }

        public void PopulateView(List<string> eventCategories)
        {
            this.eventCategories = eventCategories;

            if (eventCategories.Count > 0)
            {
                _listView.itemsSource = eventCategories;

                SetSelection(0);
            }
        }

        private VisualElement MakeItem()
        {
            var label = new Label
            {
                bindingPath = "groupName"
            };
            label.AddToClassList("item-list");

            return label;
        }

        private void BindItem(VisualElement element, string category)
        {
            Label label = element as Label;
            label.text = category;
        }

        private void SetSelection(int index)
        {
            if (_listView.itemsSource != null && _listView.itemsSource.Count > index)
                _listView.SetSelection(index);
        }

        private void OnGroupSelected(IEnumerable<object> itemsSelected)
        {
            int groupIndex = _listView.selectedIndex;
            onGroupSelected?.Invoke(groupIndex);
        }
    }
}
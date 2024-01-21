using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventGroupSelectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventGroupSelectorView, UxmlTraits> { }
        
        private const string UxmlPath = "UXML/GroupSelector.uxml";
    
        public Action<string> onCategorySelected;
    
        // Data
        private List<string> eventCategories;
    
        // Visual elements
        private VisualElement _groupsContainer;
        private EventGroupListView _groupListView;

        public int GroupIndexSelected { get; private set; }
    
        public EventGroupSelectorView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
        
            // Get references
            _groupsContainer = this.Q<VisualElement>("groups-container");
            var groupButtons = this.Q<VisualElement>("group-selector__buttons");
            groupButtons.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            
            _groupListView = new EventGroupListView();
            _groupsContainer.Add(_groupListView);
        
            RegisterCallbacks();
        }    
    
        private void RegisterCallbacks()
        {
            _groupListView.onGroupSelected += OnGroupSelected; 
        }
    
        public void PopulateView(List<string> eventCategories)
        {
            this.eventCategories = eventCategories;
        
            _groupListView.PopulateView(eventCategories);
        }
    
        private void OnGroupSelected(int groupIndex)
        {
            GroupIndexSelected = groupIndex;
        
            onCategorySelected?.Invoke(eventCategories[groupIndex]);
        }
    }
}
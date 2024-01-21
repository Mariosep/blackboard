using System.Collections.Generic;
using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventSectionView : VisualElement
    {
        private readonly string uxmlPath = "UXML/EventSection.uxml";
    
        private EventDataBase eventDataBase;
        private EventChannelInfo _eventChannelInfo;
    
        private EventGroupSelectorView _groupSelectorView;
        private EventsView _eventsView;

        public EventSectionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            style.flexGrow = 1;
        
            _groupSelectorView = this.Q<EventGroupSelectorView>();
            _eventsView = this.Q<EventsView>();
        
            RegisterCallbacks();
        }

        public void PopulateView(EventDataBase eventDataBase)
        {
            this.eventDataBase = eventDataBase;
            List<string> categories = eventDataBase.Categories;
        
            _groupSelectorView.PopulateView(categories);
        
            if (categories.Count == 0)
                _eventsView.visible = false;
        }

        private void RegisterCallbacks()
        {
            _groupSelectorView.onCategorySelected += OnCategorySelected;
            RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
        }

        private void UnregisterCallbacks()
        {
            _groupSelectorView.onCategorySelected -= OnCategorySelected;
        }

        private void OnCategorySelected(string category)
        {
            var eventChannelInfo = eventDataBase.GetEventChannelFromCategory(category);
        
            if(eventChannelInfo != null)
                ShowGroup(eventChannelInfo);
            else
                HideItemView();
        }
    
        private void ShowGroup(EventChannelInfo eventChannelInfo)
        {
            _eventChannelInfo = eventChannelInfo;
            _eventsView.PopulateView(_eventChannelInfo);
            _eventsView.visible = true;
        }
    
        private void HideItemView()
        {
            _eventsView.visible = false;
        }
    }
}
using System;
using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventsView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventsView, UxmlTraits> { }
    
        private readonly string uxmlPath = "UXML/Events.uxml";
    
        public Action<EventInfo> onEventSelected;
    
        // Data
        private EventChannelInfo _eventChannelInfo;
    
        // Visual elements
        private EventCategoryHeaderView categoryHeader;
        private VisualElement eventsContainer;
        private EventsListView eventsListView;
        private VisualElement simulatorModeContainer;

        private Button enableSimulatorModeButton; 
        
        public EventsView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            style.flexGrow = 1;
        
            categoryHeader = this.Q<EventCategoryHeaderView>();
            eventsContainer = this.Q<VisualElement>("events__container");
            simulatorModeContainer = this.Q<VisualElement>("simulator-mode");
            enableSimulatorModeButton = simulatorModeContainer.Q<Button>();
            
            RegisterCallbacks();
        }
    
        public void PopulateView(EventChannelInfo eventChannelInfo)
        {
            _eventChannelInfo = eventChannelInfo;
        
            categoryHeader.PopulateView(eventChannelInfo.category);
        
            if(eventsListView != null)
                eventsContainer.Clear(); 
        
            eventsListView = new EventsListView();        
            eventsListView.onEventSelected += OnEventSelected;
            eventsListView.PopulateView(_eventChannelInfo);
        
            eventsContainer.Add(eventsListView);
        }

        private void RegisterCallbacks()
        {
            enableSimulatorModeButton.clicked += OpenEventSimulator;
        }

        public void SelectEvent(int index)
        {
            eventsListView.SetSelection(index);
        }
    
        private void OnEventSelected(EventInfo eventSelected)
        {
            onEventSelected?.Invoke(eventSelected);
        }
        
        public void SetSimulatorButtonVisibility(bool isVisible)
        {
            simulatorModeContainer.style.display = isVisible
                ? new StyleEnum<DisplayStyle>(DisplayStyle.Flex)
                : new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        private void OpenEventSimulator()
        {
            EventSimulatorEditorWindow.OpenWindow();
        }
    }
}
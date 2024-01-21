using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventsSimulatorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventsSimulatorView, UxmlTraits> { }
        
        private readonly string uxmlPath = "UXML/EventsSimulator.uxml";
        
        // Data
        private EventInfo currentEventSelected;
        private EventChannelInfo eventChannelInfo;
        
        // Visual elements
        private EventsView eventsView;        
        private EventToSimulateView eventToSimulateView;
        private EventsLogView eventsLogView;
        
        public EventsSimulatorView()
        {
            string path = Path.Combine(EventSimulatorEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
            
            eventsView = this.Q<EventsView>();
            eventToSimulateView = this.Q<EventToSimulateView>();
            eventsLogView = this.Q<EventsLogView>();
            
            Setup();
            
            RegisterCallbacks();
        }

        private void Setup()
        {
            eventsView.SetSimulatorButtonVisibility(false);
        }

        public void PopulateView(EventChannelInfo eventChannelInfo)
        {
            this.eventChannelInfo = eventChannelInfo;
            
            eventsView.PopulateView(eventChannelInfo);
            eventsView.SelectEvent(0);
        }
        
        private void RegisterCallbacks()
        {
            eventsView.onEventSelected += OnEventSelected;
        }

        private void OnEventSelected(EventInfo eventSelected)
        {
            if (currentEventSelected == eventSelected)
                return;

            currentEventSelected = eventSelected;
            eventToSimulateView.PopulateView(eventChannelInfo, eventSelected);
        }
    }
}
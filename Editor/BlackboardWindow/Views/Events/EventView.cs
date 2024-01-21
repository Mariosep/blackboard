using System;
using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventView : VisualElement
    {
        private readonly string uxmlName = "UXML/Event.uxml";

        public Action<BaseEventSO> onEventSelected;
        
        private EventDropdown eventDropdown;
        private VisualElement eventContent;
        
        private BaseEventSO @event;

        public EventView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            eventDropdown = this.Q<EventDropdown>();
            eventContent = this.Q<VisualElement>("event-content");
            
            eventDropdown.onEventSelected += SetEvent;
        }
        
        public void SetEvent(BaseEventSO eventSelected)
        {
            if(this.@event == eventSelected)
                return;

            @event = eventSelected;
            
            eventDropdown.SetEvent(eventSelected);
            UpdateEventContent();
            
            onEventSelected?.Invoke(eventSelected);
        }
        
        private void UpdateEventContent()
        {
            eventDropdown.UpdateButtonText();
            
            eventContent.Clear();
            
            var serializedEvent = new SerializedObject(@event);
            
            var iterator = serializedEvent.GetIterator();
            iterator.NextVisible(true);
            
            while (iterator.NextVisible(false))
            {
                var propertyField = new PropertyField(iterator);
                propertyField.bindingPath = iterator.name;
                eventContent.Add(propertyField);
            }
            
            eventContent.Bind(serializedEvent);
        }
    }
}
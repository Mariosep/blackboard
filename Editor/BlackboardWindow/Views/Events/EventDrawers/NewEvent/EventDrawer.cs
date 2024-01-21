using Blackboard.Events;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    [CustomPropertyDrawer(typeof(BaseEventSO), true)]
    public class EventDrawer : PropertyDrawer
    {
        private SerializedProperty eventProperty;

        private BaseEventSO eventSelected;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            eventProperty = property;
            eventSelected = (BaseEventSO) property.objectReferenceValue;

            var eventView = new EventView();
            
            if(eventSelected != null)
                eventView.SetEvent(eventSelected);

            eventView.onEventSelected += SetEvent;
            
            return eventView;
        }

        private void SetEvent(BaseEventSO newEventSelected)
        {
            eventSelected = newEventSelected;
            eventProperty.objectReferenceValue = newEventSelected;
            eventProperty.serializedObject.ApplyModifiedProperties();
            
            Debug.Log("Event selected");
        }
    }
}
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(InteractionEventTrigger))]
public class InteractionEventTriggerDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var interactableNameProperty = property.FindPropertyRelative("interactableName");
        var eventToTriggerProperty = property.FindPropertyRelative("eventToTrigger");

        EventDropdown eventDropdown = new EventDropdown();
        
        string interactableName = interactableNameProperty.stringValue;
        eventDropdown.SetName(interactableName);
        
        EventSO eventToTrigger;
        if(eventToTriggerProperty.objectReferenceValue != null)
        {
            eventToTrigger = (EventSO)eventToTriggerProperty.objectReferenceValue;
            eventDropdown.SetEvent(eventToTrigger);    
        }    
        
        return eventDropdown;
    }
}

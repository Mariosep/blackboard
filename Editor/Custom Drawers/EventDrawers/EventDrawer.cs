using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(EventSO), true)]
public class EventDrawer : PropertyDrawer
{
    protected SerializedProperty eventProperty;

    protected EventDropdown eventDropdown;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        eventProperty = property;
        
        eventDropdown = new EventDropdown();
        eventDropdown.SetName(property.name.Capitalize());

        if (property.objectReferenceValue)
            eventDropdown.SetEvent((EventSO) property.objectReferenceValue);

        eventDropdown.onEventSelected += SetEvent;
        
        eventDropdown.style.marginTop = 5;
        eventDropdown.style.marginBottom = 5;

        return eventDropdown;
    }
 
    private void SetEvent(EventSO eventSelected)
    {
        eventProperty.objectReferenceValue = eventSelected;
        eventProperty.serializedObject.ApplyModifiedProperties();
    }
}
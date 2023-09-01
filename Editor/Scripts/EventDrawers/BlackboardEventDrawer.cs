using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(BlackboardEvent<EventSO, ScriptableObject>), true)]
public class BlackboardEventDrawer : PropertyDrawer
{
    protected SerializedProperty eventProperty;

    private SerializedProperty _idProperty;
    private SerializedProperty _categoryIdProperty;
    private SerializedProperty _eventTypeProperty;
    private SerializedProperty _eventSoProperty;
    protected SerializedProperty argProperty;

    protected EventDropdown eventDropdown;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Get properties
        eventProperty = property;
        _idProperty = property.FindPropertyRelative("id");
        _categoryIdProperty = property.FindPropertyRelative("categoryId");
        _eventTypeProperty = property.FindPropertyRelative("eventType");
        _eventSoProperty = property.FindPropertyRelative("eventSO");
        argProperty = property.FindPropertyRelative("arg");

        var eventSo = (EventSO) _eventSoProperty.objectReferenceValue;
        var argSo = (BlackboardElementSO) argProperty.objectReferenceValue;
        
        eventDropdown = new EventDropdown();
        eventDropdown.SetName(property.name.Capitalize());
        
        int enumIndex = _eventTypeProperty.intValue;
        var eventType = (BlackboardEventType)enumIndex;
        
        if(eventSo != null)
            eventDropdown.SetEvent(eventSo);
        
        if(argSo != null)
            eventDropdown.SetArg(argSo);
        
        eventDropdown.SetEventType(eventType);

        eventDropdown.onEventSelected += SetEvent;
        eventDropdown.onArgSelected += SetArg;
        
        eventDropdown.style.marginTop = 5;
        eventDropdown.style.marginBottom = 10;
        
        return eventDropdown;
    }
    
    protected EventSO GetEvent(string categoryId, string eventId)
    {
        return BlackboardManager.instance.GetEvent(categoryId, eventId);
    }
    
    private void SetEvent(EventSO eventSelected)
    {
        _idProperty.stringValue = eventSelected.id;
        _categoryIdProperty.stringValue = eventSelected.groupId;

        SetEventValue(eventSelected);
        
        eventProperty.serializedObject.ApplyModifiedProperties();
    }
    
    protected virtual void SetEventValue(EventSO eventSelected) {}

    protected virtual void SetArg(ScriptableObject argSelected) {}
}
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(EventActorSO))]
public class EventActorDrawer : EventDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        base.CreatePropertyGUI(property);
        
        eventDropdown.SetEventType(EventType.Actor);

        return eventDropdown;
    }
}
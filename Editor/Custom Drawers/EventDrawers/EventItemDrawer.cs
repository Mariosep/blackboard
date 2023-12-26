using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(EventItemSO))]
public class EventItemDrawer : EventDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        base.CreatePropertyGUI(property);
        
        eventDropdown.SetEventType(EventType.Item);

        return eventDropdown;
    }
}
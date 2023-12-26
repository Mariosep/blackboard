using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EventItem))]
public class EventItemToListenDrawer : BlackboardEventDrawer
{
    protected override void SetEventValue(EventSO eventSelected)
    {
        base.SetEventValue(eventSelected);
        
        if(eventSelected is EventItemSO eventItemSelected)
        {
            EventItem eventItem = (EventItem)SerializedPropertyUtility.GetPropertyInstance(eventProperty);
            eventItem.SetEvent(eventItemSelected);

            eventProperty.serializedObject.ApplyModifiedProperties();
        }
    }

    protected override void SetArg(ScriptableObject argSelected)
    {
        base.SetArg(argSelected);

        if (argSelected is ItemSO || argSelected == null)
        {
            argProperty.objectReferenceValue = argSelected;
            
            eventProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
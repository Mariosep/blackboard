using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EventActor))]
public class EventActorDrawer : BlackboardEventDrawer
{
    protected override void SetEventValue(EventSO eventSelected)
    {
        base.SetEventValue(eventSelected);
        
        if(eventSelected is EventActorSO eventActorSelected)
        {
            EventActor eventActor = (EventActor)SerializedPropertyUtility.GetPropertyInstance(eventProperty);
            eventActor.SetEvent(eventActorSelected);

            eventProperty.serializedObject.ApplyModifiedProperties();
        }
    }
    
    protected override void SetArg(ScriptableObject argSelected)
    {
        base.SetArg(argSelected);

        if (argSelected is ActorSO || argSelected == null)
        {
            argProperty.objectReferenceValue = argSelected;
            
            eventProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
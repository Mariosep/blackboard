using System;

[Serializable]
public class EventActor : BlackboardEvent<EventActorSO, ActorSO>
{
    public EventActor()
    {
        eventType = EventType.Actor;
    }
    
    public override void AddListener(Action<ActorSO> listener)
    {
        eventSO.AddListener(OnEventInvoked);
        currentListener = listener;
    }

    public override void RemoveListener(Action<ActorSO> listener)
    {
        eventSO.RemoveListener(OnEventInvoked);
        currentListener = null;
    }

    public override void Invoke(ActorSO argument)
    {
        eventSO.Invoke(argument);
    }
}
using System;

public class EventActorSO : EventSO
{
    public Action<ActorSO> listeners;

    public void AddListener(Action<ActorSO> listener)
    {
        listeners += listener;
    }
    
    public void RemoveListener(Action<ActorSO> listener)
    {
        listeners -= listener;
    }
    
    public void Invoke(ActorSO actor)
    {
        if(actor != null)
            listeners?.Invoke(actor);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        type = EventType.Actor;
    }
}
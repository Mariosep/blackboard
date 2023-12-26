using System;

[Serializable]
public class EventItem : BlackboardEvent<EventItemSO, ItemSO>
{
    public EventItem()
    {
        eventType = EventType.Item;
    }
    
    public override void AddListener(Action<ItemSO> listener)
    {
        eventSO.AddListener(OnEventInvoked);
        currentListener = listener;
    }

    public override void RemoveListener(Action<ItemSO> listener)
    {
        eventSO.RemoveListener(OnEventInvoked);
        currentListener = null;
    }

    public override void Invoke(ItemSO argument)
    {
        eventSO.Invoke(argument);
    }
}
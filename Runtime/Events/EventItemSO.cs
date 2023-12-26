using System;

public class EventItemSO : EventSO
{
    public Action<ItemSO> listeners;
    
    public void AddListener(Action<ItemSO> listener)
    {
        listeners += listener;
    }
    
    public void RemoveListener(Action<ItemSO> listener)
    {
        listeners -= listener;
    }

    public void Invoke(ItemSO item)
    {
        if(item != null)
            listeners?.Invoke(item);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        type = EventType.Item;
    }
}
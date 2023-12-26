public abstract class EventSO : BlackboardElementSO 
{
    public EventType type;
}

public enum EventType
{
    Actor,
    Item
}

public abstract class EventSO : BlackboardElementSO 
{
    public BlackboardEventType type;
}

public enum BlackboardEventType
{
    Actor,
    Item
}

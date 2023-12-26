public class EventConditionHandler : ConditionHandler
{
    private EventConditionSO eventCondition;
    
    public EventConditionHandler(EventConditionSO eventCondition)
    {
        this.eventCondition = eventCondition;
        
        type = ConditionType.Event;
    }
    
    public override void SetElement(BlackboardElementSO elementSelected)
    {
        var eventSelected = elementSelected as EventSO;
        
        if (eventCondition.eventSo == null || eventCondition.eventSo.type != eventSelected.type)
        {
            eventCondition.eventSo = eventSelected;
            onValueTypeChanged?.Invoke();
        }
        else
        {
            eventCondition.eventSo = eventSelected;
        }
    }
}
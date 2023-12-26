public class EventCondition : Condition
{
    private EventConditionSO conditionData;
    
    public bool eventReceived;
    
    public EventCondition(EventConditionSO conditionData)
    {
        this.conditionData = conditionData;

        if (conditionData.eventSo is EventActorSO eventActor)
        {
            eventActor.AddListener(OnEventTriggered);
        }
        else if (conditionData.eventSo is EventItemSO eventItem)
        {
            eventItem.AddListener(OnEventTriggered);
        }
    }

    private void OnEventTriggered(BlackboardElementSO eventArg)
    {
        if (conditionData.EventType == EventType.Item)
        {
            if (conditionData.itemArgRequired == null || conditionData.itemArgRequired == eventArg)
            {
                eventReceived = true;
                onGoalCompleted?.Invoke();
            }
        }
        else if (conditionData.EventType == EventType.Actor)
        {
            if (conditionData.actorArgRequired == null || conditionData.actorArgRequired == eventArg)
            {
                eventReceived = true;
                onGoalCompleted?.Invoke();
            }    
        }
    }
    
    public override bool CheckConditionGoal()
    {
        return eventReceived;
    }
}
public static class ConditionHandlerFactory
{
    public static ConditionHandler CreateConditionHandler(ConditionSO condition)
    {
        switch (condition.type)
        {
            case ConditionType.Fact:
                return new FactConditionHandler(condition as FactConditionSO);
            
            case ConditionType.Event:
                return new EventConditionHandler(condition as EventConditionSO);
            
            default:
                return null;
        }
    }
}
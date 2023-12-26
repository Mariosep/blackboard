public static class ConditionFactory
{
    public static Condition CreateCondition(ConditionSO conditionData)
    {
        return conditionData switch
        {
            FactConditionSO factRequirementData => new FactCondition(factRequirementData),
            EventConditionSO eventRequirementData => new EventCondition(eventRequirementData),
            _ => null
        };
    }
}
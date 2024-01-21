namespace Blackboard.Requirement
{
    public static class ConditionFactory
    {
        public static Condition CreateCondition(ConditionSO conditionData)
        {
            return conditionData switch
            {
                FactConditionSO factConditionData => FactConditionFactory.Create(factConditionData),
                EventConditionSO eventConditionData => EventConditionFactory.Create(eventConditionData),
                _ => null
            };
        }
    }
}
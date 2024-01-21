using Blackboard.Facts;

namespace Blackboard.Requirement
{
    public static class FactConditionFactory
    {
        public static FactCondition Create(FactConditionSO factConditionData)
        {
            return factConditionData.fact.type switch
            {
                FactType.Bool => new BoolFactCondition(factConditionData),
                FactType.Int => new IntFactCondition(factConditionData),
                FactType.Float => new FloatFactCondition(factConditionData),
                FactType.String => new StringFactCondition(factConditionData),
                _ => null
            };
        }
    }
}
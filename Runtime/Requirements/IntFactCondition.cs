using System;
using Blackboard.Facts;

namespace Blackboard.Requirement
{
    [Serializable]
    public class IntFactCondition : FactCondition
    {
        private IntFactSO intFact;
    
        public IntFactCondition(FactConditionSO conditionData) : base(conditionData)
        {
            intFact = conditionData.fact as IntFactSO;
            intFact.onValueChanged += OnValueChanged;
        }

        protected override void CheckCondition()
        {
            IsFulfilled = ComparisonUtility.Compare(intFact.Value, conditionData.IntRequired, conditionData.comparisonOperator);
        }

        private void OnValueChanged(int newValue)
        {
            CheckCondition();
        }
    }
}
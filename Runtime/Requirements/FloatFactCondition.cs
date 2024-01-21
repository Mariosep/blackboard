using System;
using Blackboard.Facts;

namespace Blackboard.Requirement
{
    [Serializable]
    public class FloatFactCondition : FactCondition
    {
        private FloatFactSO floatFact;
    
        public FloatFactCondition(FactConditionSO conditionData) : base(conditionData)
        {
            floatFact = conditionData.fact as FloatFactSO;
            floatFact.onValueChanged += OnValueChanged;
        }

        protected override void CheckCondition()
        {
            IsFulfilled = ComparisonUtility.Compare(floatFact.Value, conditionData.FloatRequired, conditionData.comparisonOperator);
        }

        private void OnValueChanged(float newValue)
        {
            CheckCondition();
        }
    }
}
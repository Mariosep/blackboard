using Blackboard.Facts;

namespace Blackboard.Requirement
{
    public class StringFactCondition : FactCondition
    {
        private StringFactSO stringFact;
    
        public StringFactCondition(FactConditionSO conditionData) : base(conditionData)
        {
            stringFact = conditionData.fact as StringFactSO;
            stringFact.onValueChanged += OnValueChanged;
        }

        protected override void CheckCondition()
        {
            IsFulfilled = ComparisonUtility.Compare(stringFact.Value, conditionData.StringRequired, conditionData.comparisonOperator);
        }

        private void OnValueChanged(string newValue)
        {
            CheckCondition();
        }
    }
}
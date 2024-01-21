using Blackboard.Facts;

namespace Blackboard.Requirement
{
    public class BoolFactCondition : FactCondition
    {
        private BoolFactSO boolFact;
    
        public BoolFactCondition(FactConditionSO conditionData) : base(conditionData)
        {
            boolFact = conditionData.fact as BoolFactSO;
            boolFact.onValueChanged += OnValueChanged;
        }

        protected override void CheckCondition()
        {
            IsFulfilled = ComparisonUtility.Compare(boolFact.Value, conditionData.BoolRequired, conditionData.comparisonOperator);
        }

        private void OnValueChanged(bool newValue)
        {
            CheckCondition();
        }
    }
}
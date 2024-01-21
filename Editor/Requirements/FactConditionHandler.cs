using Blackboard.Facts;
using Blackboard.Requirement;

namespace Blackboard.Editor.Requirement
{
    public class FactConditionHandler : ConditionHandler
    {
        private FactConditionSO factCondition;

        public FactConditionHandler(FactConditionSO factCondition)
        {
            this.factCondition = factCondition;
        
            type = ConditionType.Fact;
        }
    
        public override void SetElement(BlackboardElementSO elementSelected)
        {
            var factSelected = elementSelected as FactSO;
        
            if (factCondition.fact == null || factCondition.fact.type != factSelected.type)
            {
                factCondition.fact = factSelected;
                onValueTypeChanged?.Invoke();
            }
            else
            {
                factCondition.fact = factSelected;
            }
        }
    }
}
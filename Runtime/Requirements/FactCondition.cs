using System;

namespace Blackboard.Requirement
{
    [Serializable]
    public abstract class FactCondition : Condition
    {
        protected FactConditionSO conditionData;

        public FactCondition(FactConditionSO conditionData) : base(conditionData)
        {
            this.conditionData = conditionData;
        }

        public override void Enable()
        {
            CheckCondition();
        }

        protected abstract void CheckCondition();
    }
}
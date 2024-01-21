using System;
using Blackboard.Requirement;

namespace Blackboard.Editor.Requirement
{
    public abstract class ConditionHandler
    {
        public Action onValueTypeChanged;
    
        public ConditionType type;

        public abstract void SetElement(BlackboardElementSO elementSelected);
    }
}
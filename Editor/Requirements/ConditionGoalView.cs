using Blackboard.Requirement;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public abstract class ConditionGoalView : VisualElement
    {
        public abstract void BindCondition(ConditionSO condition);
    }
}
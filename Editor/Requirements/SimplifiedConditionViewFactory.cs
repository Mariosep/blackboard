using Blackboard.Requirement;

namespace Blackboard.Editor.Requirement
{
    public static class SimplifiedConditionViewFactory
    {
        public static SimplifiedConditionView CreateConditionView(ConditionSO condition)
        {
            switch (condition.type)
            {
                case ConditionType.Fact:
                    return new SimplifiedFactConditionView();
                
                case ConditionType.Event:
                    return new SimplifiedEventConditionView();
            }

            return null;
        }
    }
}
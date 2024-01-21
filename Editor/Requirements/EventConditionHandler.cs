using Blackboard.Events;
using Blackboard.Requirement;

namespace Blackboard.Editor.Requirement
{
    public class EventConditionHandler : ConditionHandler
    {
        private EventConditionSO eventCondition;
    
        public EventConditionHandler(EventConditionSO eventCondition)
        {
            this.eventCondition = eventCondition;
        
            type = ConditionType.Event;
        }
    
        public override void SetElement(BlackboardElementSO elementSelected)
        {
            var eventSelected = elementSelected as BaseEventSO;
        
            eventCondition.eventRequired = eventSelected;
            onValueTypeChanged?.Invoke();
        }
    }
}
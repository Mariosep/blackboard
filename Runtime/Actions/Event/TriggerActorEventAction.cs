using System.Collections;

namespace Blackboard.Actions
{
    [Category("Event")]
    public class TriggerActorEventAction : Action
    {
        public EventActorSO eventToTrigger;
        public ActorSO actor;

        public override string GetName() => "Trigger Actor Event";
    
        public override IEnumerator Execute()
        {
            eventToTrigger.Invoke(actor);
            
            yield return null;
        }
    }
    
    [Category("Event")]
    public class TriggerItemEventAction : Action
    {
        public EventItemSO eventToTrigger;
        public ItemSO item;

        public override string GetName() => "Trigger Item Event";
    
        public override IEnumerator Execute()
        {
            eventToTrigger.Invoke(item);
            
            yield return null;
        }
    }
}

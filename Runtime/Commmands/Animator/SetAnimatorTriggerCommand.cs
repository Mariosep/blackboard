using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Animator")]
    public class SetAnimatorTriggerCommand : Command
    {
        public Animator animator;
        public string triggerName;
        public override string GetName() => "Set Animator Trigger";

        public override IEnumerator Execute()
        {
            if (animator != null)
                animator.SetTrigger(triggerName);
            
            yield return null;
        }
    }
}
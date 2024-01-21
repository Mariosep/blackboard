using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Animator")]
    public class SetAnimatorBoolCommand : Command
    {
        public Animator animator;
        public string boolName;
        public bool newValue;
        
        public override string GetName() => "Set Animator Bool";

        public override IEnumerator Execute()
        {
            if (animator != null)
                animator.SetBool(boolName, newValue);
            
            yield return null;
        }
    }
}
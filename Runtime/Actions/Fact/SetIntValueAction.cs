using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class SetIntValueAction : Action
    {
        public IntFactSO intFact;
        public int value;
        
        public override string GetName() => "Set Int Value";
    
        public override IEnumerator Execute()
        {
            Debug.Log(GetName());

            if(intFact != null)
                intFact.Value = value;
            
            yield return null;
        }
    }
}
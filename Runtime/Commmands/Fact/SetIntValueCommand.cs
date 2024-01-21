using System.Collections;
using Blackboard.Facts;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class SetIntValueCommand : Command
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
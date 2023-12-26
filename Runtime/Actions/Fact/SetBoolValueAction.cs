using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class SetBoolValueAction : Action
    {
        public BoolFactSO boolFact;
        public bool value;
        
        public override string GetName() => "Set Bool Value";
    
        public override IEnumerator Execute()
        {
            if(boolFact != null)
                boolFact.Value = value;
            
            yield return null;
        }
    }
}

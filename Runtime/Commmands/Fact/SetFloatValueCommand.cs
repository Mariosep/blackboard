using System.Collections;
using Blackboard.Facts;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class SetFloatValueCommand : Command
    {
        public FloatFactSO floatFact;
        public float value;
        
        public override string GetName() => "Set Float Value";
    
        public override IEnumerator Execute()
        {
            Debug.Log(GetName());

            if(floatFact != null)
                floatFact.Value = value;
            
            yield return null;
        }
    }
}
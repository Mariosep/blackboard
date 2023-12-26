using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class SetFloatValueAction : Action
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
using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class SetStringValueAction : Action
    {
        public StringFactSO stringFact;
        public string value;
        
        public override string GetName() => "Set String Value";
    
        public override IEnumerator Execute()
        {
            Debug.Log(GetName());

            if(stringFact != null)
                stringFact.Value = value;
            
            yield return null;
        }
    }
}
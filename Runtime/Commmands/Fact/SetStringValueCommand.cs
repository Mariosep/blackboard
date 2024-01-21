using System.Collections;
using Blackboard.Facts;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class SetStringValueCommand : Command
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
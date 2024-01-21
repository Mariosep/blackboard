using System.Collections;
using Blackboard.Facts;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class ToggleBoolValueCommand : Command
    {
        public BoolFactSO boolFact;
        
        public override string GetName() => "Toggle Bool Value";
    
        public override IEnumerator Execute()
        {
            if(boolFact != null)
                boolFact.Value = !boolFact.Value;
            
            yield return null;
        }
    }
}
using System.Collections;
using Blackboard.Facts;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class SetBoolValueCommand : Command
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

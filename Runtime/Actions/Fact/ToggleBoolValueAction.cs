using System.Collections;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class ToggleBoolValueAction : Action
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
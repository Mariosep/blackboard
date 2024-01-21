using System.Collections;
using Blackboard.Facts;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class AddFloatValueFactCommand : Command
    {
        public FloatFactSO floatFact;
        public float valueToAdd;
        
        public override string GetName() => "Add Float Value";
    
        public override IEnumerator Execute()
        {
            if(floatFact != null)
                floatFact.Value += valueToAdd;
            
            yield return null;
        }
    }
}
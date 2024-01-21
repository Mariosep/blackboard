using System.Collections;
using Blackboard.Facts;

namespace Blackboard.Commands
{
    [Category("Fact")]
    public class SubtractIntegerValueFactCommand : Command
    {
        public IntFactSO intFact;
        public int valueToAdd;
        
        public override string GetName() => "Subtract Integer Value";
    
        public override IEnumerator Execute()
        {
            if(intFact != null)
                intFact.Value -= valueToAdd;
            
            yield return null;
        }
    }
}
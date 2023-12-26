using System.Collections;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class SubtractIntegerValueFactAction : Action
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
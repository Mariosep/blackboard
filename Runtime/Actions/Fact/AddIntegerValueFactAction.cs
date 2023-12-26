using System.Collections;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class AddIntegerValueFactAction : Action
    {
        public IntFactSO intFact;
        public int valueToAdd;
        
        public override string GetName() => "Add Integer Value";
    
        public override IEnumerator Execute()
        {
            if(intFact != null)
                intFact.Value += valueToAdd;
            
            yield return null;
        }
    }
}
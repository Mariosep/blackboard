using System.Collections;

namespace Blackboard.Actions
{
    [Category("Fact")]
    public class AddFloatValueFactAction : Action
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
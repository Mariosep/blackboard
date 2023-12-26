using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("GameObject")]
    public class DisableGameObject : Action
    {
        public GameObject gameObject;
        
        public override string GetName() => "Disable GameObject";

        public override IEnumerator Execute()
        {
            if (gameObject != null)
                gameObject.SetActive(false);
            
            yield return null;
        }
    }
}
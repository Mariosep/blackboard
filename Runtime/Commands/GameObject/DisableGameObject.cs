using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("GameObject")]
    public class DisableGameObject : Command
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
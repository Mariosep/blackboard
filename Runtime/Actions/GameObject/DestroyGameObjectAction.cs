using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("GameObject")]
    public class DestroyGameObjectAction : Action
    {
        public GameObject gameObject;
        public override string GetName() => "Destroy GameObject";

        public override IEnumerator Execute()
        {
            if (gameObject != null)
                Destroy(gameObject);

            yield return null;
        }
    }
}

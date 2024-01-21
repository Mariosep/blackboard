using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("GameObject")]
    public class DestroyGameObjectCommand : Command
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

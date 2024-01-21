using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("GameObject")]
    public class InstantiateGameObjectCommand : Command
    {
        public GameObject gameObject;
        public Vector3 position;
        public Vector3 rotation;
        public override string GetName() => "Instantiate GameObject";

        public override IEnumerator Execute()
        {
            if (gameObject != null)
                Instantiate(gameObject, position, Quaternion.Euler(rotation));
            
            yield return null;
        }
    }
}
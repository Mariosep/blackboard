using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract string GetName();
    
        public abstract IEnumerator Execute();
    }
}

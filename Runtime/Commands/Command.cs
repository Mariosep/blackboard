using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    public abstract class Command : ScriptableObject
    {
        public abstract string GetName();
    
        public abstract IEnumerator Execute();
    }
}

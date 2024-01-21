using System.Collections;
using System.Collections.Generic;
using Blackboard.Utils;
using UnityEngine;

namespace Blackboard.Commands
{
    public class CommandList : ScriptableObject
    {
        public string id;

        public List<Command> commands = new List<Command>();
        
        public virtual void Init(string id)
        {
            this.id = id;
            name = $"commandList-{id}";
            commands = new List<Command>();
        }

        public void Execute()
        {
            CoroutineManager.Instance.StartCoroutineFromManager(Co_Execute());
        }

        public IEnumerator Co_Execute()
        {
            foreach (Command command in commands)
            {
                yield return command.Execute();
            }
        }

        public void Reset()
        {
            Init(id);
        }
    }
}
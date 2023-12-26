using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blackboard.Actions
{
    public class ActionList : ScriptableObject
    {
        public string id;

        public List<Action> actions = new List<Action>();
        
        public virtual void Init(string id)
        {
            this.id = id;
            name = $"actionList-{id}";
            actions = new List<Action>();
        }

        public void Execute()
        {
            CoroutineManager.Instance.StartCoroutineFromManager(Co_Execute());
        }

        public IEnumerator Co_Execute()
        {
            foreach (Action action in actions)
            {
                yield return action.Execute();
            }
        }
    }
}
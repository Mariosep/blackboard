using System;
using UnityEngine;

namespace Blackboard.Requirement
{
    public abstract class ConditionSO : ScriptableObject
    {
        public Action onConditionModified; 
        
        public string id;

        public ConditionType type;

        public virtual void Init(string id)
        {
            this.id = id;
            name = $"condition-{id}";
        }

        public abstract void SetElementRequired(BlackboardElementSO elementRequired);
        public abstract BlackboardElementSO GetElementRequired();

        public abstract void SaveAs(ScriptableObject mainAsset);
    
        public ConditionSO Clone(string newId)
        {
            ConditionSO clonedCondition = Instantiate(this);
            clonedCondition.id = newId;
            clonedCondition.name = $"condition-{newId}";

            return clonedCondition;
        }
    }

    public enum ConditionType
    {
        Fact,
        Event
    }
}
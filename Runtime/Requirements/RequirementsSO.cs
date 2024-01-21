using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blackboard.Requirement
{
    public class RequirementsSO : ScriptableObject
    {
        public Action onRequirementsModified;
        
        public string id;

        public List<ConditionSO> conditions = new List<ConditionSO>();

        public virtual void Init(string id)
        {
            this.id = id;
            name = $"requirements-{id}";
            conditions = new List<ConditionSO>();
        }

        public void AddCondition(ConditionSO newCondition)
        {
            conditions.Add(newCondition);
            
            onRequirementsModified?.Invoke();
        }
        
        public void RemoveCondition(ConditionSO conditionToRemove)
        {
            conditions.Remove(conditionToRemove);
            
            onRequirementsModified?.Invoke();
        }

        public void ReplaceCondition(ConditionSO newCondition, int i)
        {
            conditions[i] = newCondition;
            
            onRequirementsModified?.Invoke();
        }
        
        public RequirementsSO Clone(string newId)
        {
            RequirementsSO clonedRequirements = Instantiate(this);
            clonedRequirements.id = newId;
            clonedRequirements.name = $"requirements-{newId}";

            clonedRequirements.conditions = new List<ConditionSO>();
            foreach (ConditionSO cond in conditions)
            {
                clonedRequirements.conditions.Add(cond.Clone(newId));
            }
        
            return clonedRequirements;
        }

        public void Reset()
        {
            Init(id);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

public class RequirementsSO : ScriptableObject
{
    public string id;

    public List<ConditionSO> conditions = new List<ConditionSO>();

    public virtual void Init(string id)
    {
        this.id = id;
        name = $"requirements-{id}";
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
}
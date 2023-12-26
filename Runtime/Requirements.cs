using System.Collections.Generic;

public class Requirements
{
    public List<Condition> conditions;

    public Requirements(RequirementsSO requirementsData)
    {
        conditions = new List<Condition>();
        
        foreach (ConditionSO conditionData in requirementsData.conditions)
        {
            Condition condition = ConditionFactory.CreateCondition(conditionData);
            if(condition != null)
                conditions.Add(condition);
        }
    }

    public bool CheckRequirementsGoal()
    {
        foreach (Condition cond in conditions)
        {
            if (!cond.CheckConditionGoal())
                return false;
        }

        return true;
    }
}
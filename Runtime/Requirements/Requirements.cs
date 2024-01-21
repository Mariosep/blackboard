using System;
using System.Collections.Generic;

namespace Blackboard.Requirement
{
    public class Requirements
    {
        public Action onFulfilled;
        public Action<Condition> onConditionFulfilled;
        public Action<Condition> onConditionUnfulfilled;
    
        public List<Condition> conditions;
        private List<Condition> conditionsFulfilled;

        private bool areFulfilled;
        public bool AreFulfilled => areFulfilled;
    
        public Requirements(RequirementsSO requirementsData)
        {
            conditions = new List<Condition>();
            conditionsFulfilled = new List<Condition>();
        
            foreach (ConditionSO conditionData in requirementsData.conditions)
            {
                Condition condition = ConditionFactory.CreateCondition(conditionData);
                if(condition != null)
                {
                    conditions.Add(condition);
                    condition.onFulfilled += OnConditionFulfilled;
                    condition.onUnfulfilled += OnConditionUnfulfilled;
                }
            }

            foreach (Condition condition in conditions)
                condition.Enable();
        }
    
        private void OnConditionFulfilled(Condition condition)
        {
            conditionsFulfilled.Add(condition);
            onConditionFulfilled?.Invoke(condition);
            
            if(conditions.Count == conditionsFulfilled.Count)
            {
                areFulfilled = true;
                onFulfilled?.Invoke();
            }
        }
    
        private void OnConditionUnfulfilled(Condition condition)
        {
            if(conditionsFulfilled.Contains(condition))
                conditionsFulfilled.Remove(condition);
            
            onConditionUnfulfilled?.Invoke(condition);
        }
    }
}
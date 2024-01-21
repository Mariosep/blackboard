using System;
using UnityEngine;

namespace Blackboard.Requirement
{
    [Serializable]
    public abstract class Condition
    {
        public Action<Condition> onFulfilled;
        public Action<Condition> onUnfulfilled;

        public ConditionSO conditionData;
        
        [SerializeField] private bool isFulfilled;

        public bool IsFulfilled
        {
            get => isFulfilled;
            protected set
            {
                if (isFulfilled == value)
                    return;

                isFulfilled = value;

                if (isFulfilled)
                    onFulfilled?.Invoke(this);
                else
                    onUnfulfilled?.Invoke(this);
            }
        }

        public Condition(ConditionSO conditionData)
        {
            this.conditionData = conditionData;
        }
        
        public virtual void Enable()
        {
        }
    }
}
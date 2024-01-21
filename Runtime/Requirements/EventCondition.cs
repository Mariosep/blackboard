using System;
using System.Collections.Generic;
using System.Reflection;
using Blackboard.Events;

namespace Blackboard.Requirement
{
    [Serializable]
    public class EventCondition : Condition
    {
        protected EventConditionSO conditionData;
        protected BaseEventSO eventData => conditionData.eventRequired;
        
        public EventCondition(EventConditionSO conditionData) : base(conditionData)
        {
            this.conditionData = conditionData;
                
            Type channelType = Type.GetType($"Blackboard.Events.{eventData.eventInfo.category}EventChannel, Mariosep.Blackboard");
            var getMethod = typeof(ServiceLocator).GetMethod("Get").MakeGenericMethod(channelType);
            object result = getMethod.Invoke(null, null);

            EventChannel eventChannel = (EventChannel)result;
            LinkToChannel(eventChannel);
        }

        public virtual void LinkToChannel(EventChannel eventChannel)
        {
            eventChannel.LinkEvent(conditionData.eventRequired, OnEventTriggered);
        }

        private void OnEventTriggered()
        {
            IsFulfilled = true;
        }
    }
    
    [Serializable]
    public class EventCondition<T1> : EventCondition
    {
        public EventCondition(EventConditionSO eventData): base(eventData) {}

        public override void LinkToChannel(EventChannel eventChannel)
        {
            eventChannel.LinkEvent<T1>(conditionData.eventRequired, OnEventTriggered);
        }

        private void OnEventTriggered(T1 arg1)
        {
            if(ArgumentValuesRequiredFulfilled(arg1))
            {
                IsFulfilled = true;
            }
        }
        
        private bool ArgumentValuesRequiredFulfilled(T1 arg1)
        {
            FieldInfo[] fields = eventData.GetType().GetFields();

            bool valuesAreEqual = true;
            
            if (fields.Length > 0)
            {
                if(conditionData.paramValuesRequired[0])
                {
                    FieldInfo firstField = fields[0];
                    T1 firstFieldValue = (T1)firstField.GetValue(eventData);

                    valuesAreEqual = EqualityComparer<T1>.Default.Equals(firstFieldValue, arg1);
                }
                return valuesAreEqual;
            }

            Console.WriteLine("No fields found in the type.");
            return false;
        }
    }
    
    [Serializable]
    public class EventCondition<T1, T2> : EventCondition
    {
        public EventCondition(EventConditionSO eventData): base(eventData) {}

        public override void LinkToChannel(EventChannel eventChannel)
        {
            eventChannel.LinkEvent<T1, T2>(conditionData.eventRequired, OnEventTriggered);
        }
        
        private void OnEventTriggered(T1 arg1, T2 arg2)
        {
            if(ArgumentValuesRequiredFulfilled(arg1, arg2))
            {
                IsFulfilled = true;
            }
        }
        
        private bool ArgumentValuesRequiredFulfilled(T1 arg1, T2 arg2)
        {
            FieldInfo[] fields = eventData.GetType().GetFields();

            bool valuesAreEqual = true;
            
            if (fields.Length > 0)
            {
                if(conditionData.paramValuesRequired[0])
                {
                    FieldInfo firstField = fields[0];
                    T1 firstFieldValue = (T1)firstField.GetValue(eventData);
                    valuesAreEqual = EqualityComparer<T1>.Default.Equals(firstFieldValue, arg1);

                    if (!valuesAreEqual)
                        return false;
                }        
                if(conditionData.paramValuesRequired[1])
                {
                    FieldInfo secondField = fields[1];
                    T2 secondFieldValue = (T2)secondField.GetValue(eventData);
                    valuesAreEqual = EqualityComparer<T2>.Default.Equals(secondFieldValue, arg2);
                    
                    if (!valuesAreEqual)
                        return false;
                }
                
                return valuesAreEqual;
            }

            Console.WriteLine("No fields found in the type.");
            return false;
        }
    }
}
using System;
using System.Reflection;
using Blackboard.Requirement;
using UnityEditor;
using UnityEditor.UIElements; 

namespace Blackboard.Editor.Requirement
{
    public class ConditionGoalEventView : ConditionGoalView
    {
        private EventConditionSO eventCondition;

        public override void BindCondition(ConditionSO condition)
        {
            if(eventCondition == condition)
                return;
        
            eventCondition = (EventConditionSO) condition;
        
            UpdateEventContent();
        }
    
        private void UpdateEventContent()
        {
            this.Clear();
            
            SerializedObject serializedEvent = new SerializedObject(eventCondition.eventRequired);
            SerializedObject serializedCondition = new SerializedObject(eventCondition);
            SerializedProperty serializedParamValuesRequired = serializedCondition.FindProperty("paramValuesRequired");
        
            var iterator = serializedEvent.GetIterator();
            iterator.NextVisible(true);

            int paramCount = 0;
            while (iterator.NextVisible(false))
            {
                var field = eventCondition.eventRequired.GetType()
                    .GetField(iterator.name, BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                {
                    // Check if the field has the ParameterAttribute
                    if (Attribute.IsDefined(field, typeof(ParameterAttribute)))
                    {
                        SerializedProperty paramValueRequired = serializedParamValuesRequired.GetArrayElementAtIndex(paramCount);
                    
                        var eventParamCondition = new EventParamConditionView();
                        eventParamCondition.BindArgument(eventCondition, paramValueRequired, iterator);
                        this.Add(eventParamCondition);

                        paramCount++;
                    }
                }
            }

            this.Bind(serializedEvent);
        }
    }
}
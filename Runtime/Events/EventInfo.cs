using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blackboard.Events
{
    [Serializable]
    public struct EventInfo : IEquatable<EventInfo>
    {
        public string category;
        public Type eventType;
        public string eventName;
        public List<ParameterInfo> parameters;
        public string description;

        public EventInfo(Type eventType)
        {
            this.eventType = eventType;
            eventName = eventType.Name.AddSpaceBeforeCapitalLetters();
            
            var categoryAttribute = CustomAttributeExtensions.GetCustomAttribute<CategoryAttribute>(eventType);
            category = categoryAttribute?.Category ?? "Uncategorized";
            
            FieldInfo[] fields = eventType.GetFields();
            parameters = new List<ParameterInfo>();
            
            for (var i = 0; i < fields.Length; i++)
            {
                if (Attribute.IsDefined(fields[i], typeof(ParameterAttribute)))
                {
                    var argumentInfo = new ParameterInfo(fields[i].FieldType, fields[i].Name);
                    parameters.Add(argumentInfo);     
                }
            }
            
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(eventType, typeof(DescriptionAttribute));
            description = descriptionAttribute != null ? descriptionAttribute.Description : "";
        }

        #region Comparison
        public bool Equals(EventInfo other)
        {
            return eventName == other.eventName;
        }

        public override bool Equals(object obj)
        {
            return obj is EventInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(eventType, eventName, parameters, description);
        }
        
        // Custom comparison methods
        public static bool operator ==(EventInfo left, EventInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EventInfo left, EventInfo right)
        {
            return !left.Equals(right);
        }
        #endregion
    }

    public struct EventInvocationInfo
    {
        public EventInfo eventInfo;
        public object[] paramsValues;

        public string EventName => eventInfo.eventName;
        
        public EventInvocationInfo(EventInfo eventInfo, object[] paramsValues)
        {
            this.eventInfo = eventInfo;
            this.paramsValues = paramsValues;
        }
    }
}
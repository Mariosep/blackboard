using System;
using System.Reflection;

namespace Blackboard.Events
{
    public abstract class BaseEventSO : BlackboardElementSO
    {
        public override BlackboardElementType BlackboardElementType => BlackboardElementType.Event;
        public string EventName => GetType().Name;
        public abstract int ParametersCount { get; }
        public abstract Type[] ParametersTypes { get; }
        
        public EventInfo eventInfo;
        
        public object GetParameterValue(int parameterIndex)
        {
            string paramName = eventInfo.parameters[parameterIndex].parameterName;
            FieldInfo paramField = GetType().GetField(paramName, BindingFlags.Public | BindingFlags.Instance);
            object param = paramField?.GetValue(this);

            return param;
        }
        
        public override void Init(string id)
        {
            this.id = id;
            name = $"event-{id}";
            eventInfo = new EventInfo(GetType());
        }

        public string GetName()
        {
            Name ??= this.GetType().Name.AddSpaceBeforeCapitalLetters();
            return Name;
        }
    }
    
    public abstract class EventSO : BaseEventSO
    {
        // Properties
        //public override BlackboardElementType BlackboardElementType => BlackboardElementType.Event;
        //public string EventName => GetType().Name;
        public override int ParametersCount => 0;
        public override Type[] ParametersTypes => Type.EmptyTypes;

        //public EventInfo eventInfo;

        /*public object GetParameterValue(int parameterIndex)
        {
            string paramName = eventInfo.parameters[parameterIndex].parameterName;
            FieldInfo paramField = GetType().GetField(paramName, BindingFlags.Public | BindingFlags.Instance);
            object param = paramField?.GetValue(this);

            return param;
        }*/

        // Methods
        /*public override void Init(string id)
        {
            this.id = id;
            name = $"event-{id}";
            eventInfo = new EventInfo(GetType());
        }*/

        /*public string GetName()
        {
            Name ??= this.GetType().Name.AddSpaceBeforeCapitalLetters();
            return Name;
        }*/

        public void Invoke()
        {
            Type channelType = Type.GetType($"Blackboard.Events.{eventInfo.category}EventChannel, Mariosep.Blackboard");
            var getMethod = typeof(ServiceLocator).GetMethod("Get").MakeGenericMethod(channelType);
            object result = getMethod.Invoke(null, null);
            
            FieldInfo actionToInvokeField = result.GetType().GetField(EventName.ToCamelCase(),
                BindingFlags.Public | BindingFlags.Instance);

            if (actionToInvokeField != null && typeof(Delegate).IsAssignableFrom(actionToInvokeField.FieldType))
            {
                Delegate actionDelegate = (Delegate)actionToInvokeField.GetValue(this);
                actionDelegate?.DynamicInvoke();

                //eventInvocationInfo = new EventInvocationInfo(eventInfo, null);
                //onEventInvoked?.Invoke(eventInvocationInfo);
            }
        }
    }

    public abstract class EventSO<T1, T2> : BaseEventSO
    {
        public override int ParametersCount => 2;
        public override Type[] ParametersTypes => new[] { typeof(T1), typeof(T2) };
        
        public void Invoke(T1 param1, T2 param2)
        {
            Type channelType = Type.GetType($"Blackboard.Events.{eventInfo.category}EventChannel, Mariosep.Blackboard");
            var getMethod = typeof(ServiceLocator).GetMethod("Get").MakeGenericMethod(channelType);
            object result = getMethod.Invoke(null, null);
            EventChannel eventChannel = (EventChannel)result;
            eventChannel.InvokeEvent(this);
            
            /*FieldInfo actionToInvokeField = result.GetType().GetField(EventName.ToCamelCase(),
                BindingFlags.Public | BindingFlags.Instance);

            if (actionToInvokeField != null && typeof(Delegate).IsAssignableFrom(actionToInvokeField.FieldType))
            {
                Delegate actionDelegate = (Delegate)actionToInvokeField.GetValue(this);
                // var param = GetParameterValue(0);
                actionDelegate?.DynamicInvoke(param1, param2);

                //eventInvocationInfo = new EventInvocationInfo(eventInfo, new[] { param });
                //onEventInvoked?.Invoke(eventInvocationInfo);
            }*/
        }
    }

    public abstract class EventSO<T1> : BaseEventSO
    {
        public override int ParametersCount => 1;

        public override Type[] ParametersTypes => new[] { typeof(T1) };
        
        public void Invoke(T1 param1)
        {
            Type channelType = Type.GetType($"Blackboard.Events.{eventInfo.category}EventChannel, Mariosep.Blackboard");
            var getMethod = typeof(ServiceLocator).GetMethod("Get").MakeGenericMethod(channelType);
            object result = getMethod.Invoke(null, null);
            EventChannel eventChannel = (EventChannel)result;
            eventChannel.InvokeEvent(this);
            
            /*FieldInfo actionToInvokeField = result.GetType().GetField(EventName.ToCamelCase(),
                BindingFlags.Public | BindingFlags.Instance);

            if (actionToInvokeField != null && typeof(Delegate).IsAssignableFrom(actionToInvokeField.FieldType))
            {
                Delegate actionDelegate = (Delegate)actionToInvokeField.GetValue(result);
                // var param = GetParameterValue(0);
                actionDelegate?.DynamicInvoke(param1);

                //eventInvocationInfo = new EventInvocationInfo(eventInfo, new[] { param });
                //onEventInvoked?.Invoke(eventInvocationInfo);
            }*/
        }
    }
}
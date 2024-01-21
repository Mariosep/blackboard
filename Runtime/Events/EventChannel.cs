using System;
using System.Reflection;

namespace Blackboard.Events
{
    public class EventChannel
    {
        public static Action<EventInvocationInfo> onEventInvoked;

        public void InvokeEvent(BaseEventSO eventData)
        {
            FieldInfo actionToInvokeField = GetType().GetField(eventData.EventName.ToCamelCase(),
                BindingFlags.Public | BindingFlags.Instance);

            if (actionToInvokeField != null && typeof(Delegate).IsAssignableFrom(actionToInvokeField.FieldType))
            {
                Delegate actionDelegate = (Delegate)actionToInvokeField.GetValue(this);
                EventInvocationInfo eventInvocationInfo;

                switch (eventData.ParametersCount)
                {
                    case 0:
                        actionDelegate?.DynamicInvoke();

                        eventInvocationInfo = new EventInvocationInfo(eventData.eventInfo, null);
                        onEventInvoked?.Invoke(eventInvocationInfo);
                        break;

                    case 1:
                        var param = eventData.GetParameterValue(0);
                        actionDelegate?.DynamicInvoke(param);

                        eventInvocationInfo = new EventInvocationInfo(eventData.eventInfo, new[] { param });
                        onEventInvoked?.Invoke(eventInvocationInfo);
                        break;

                    case 2:
                        object param1 = eventData.GetParameterValue(0);
                        object param2 = eventData.GetParameterValue(1);

                        actionDelegate?.DynamicInvoke(param1, param2);

                        eventInvocationInfo = new EventInvocationInfo(eventData.eventInfo, new[] { param1, param2 });
                        onEventInvoked?.Invoke(eventInvocationInfo);
                        break;
                }
            }
        }

        public void LinkEvent(BaseEventSO eventToLink, Action callback)
        {
            string eventName = eventToLink.EventName.ToCamelCase();
            FieldInfo fieldInfo = GetType().GetFieldInfo(eventName);

            if (fieldInfo != null)
            {
                // Get the field value
                object fieldValue = fieldInfo.GetValue(this);

                if (fieldValue is Action action)
                {
                    action += callback;
                    fieldInfo.SetValue(this, action);
                }
                else
                {
                    Action newAction = callback;
                    fieldInfo.SetValue(this, newAction);
                }
            }
            else
            {
                Console.WriteLine($"Field '{eventName}' not found");
            }
        }

        public void LinkEvent<T1>(BaseEventSO eventToLink, Action<T1> callback)
        {
            string eventName = eventToLink.EventName.ToCamelCase();
            FieldInfo fieldInfo = GetType().GetFieldInfo(eventName);

            if (fieldInfo != null)
            {
                // Get the field value
                object fieldValue = fieldInfo.GetValue(this);

                if (fieldValue is Action<T1> action)
                {
                    action += callback;
                    fieldInfo.SetValue(this, action);
                }
                else
                {
                    Action<T1> newAction = callback;
                    fieldInfo.SetValue(this, newAction);
                }
            }
            else
            {
                Console.WriteLine($"Field '{eventName}' not found");
            }
        }

        public void LinkEvent<T1, T2>(BaseEventSO eventToLink, Action<T1, T2> callback)
        {
            string eventName = eventToLink.EventName.ToCamelCase();
            FieldInfo fieldInfo = GetType().GetFieldInfo(eventName);

            if (fieldInfo != null)
            {
                // Get the field value
                object fieldValue = fieldInfo.GetValue(this);

                if (fieldValue is Action<T1, T2> action)
                {
                    action += callback;
                    fieldInfo.SetValue(this, action);
                }
                else
                {
                    Action<T1, T2> newAction = callback;
                    fieldInfo.SetValue(this, newAction);
                }
            }
            else
            {
                Console.WriteLine($"Field '{eventName}' not found");
            }
        }
    }
}
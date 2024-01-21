using System;
using Blackboard.Events;

namespace Blackboard.Requirement
{
    public static class EventConditionFactory
    {
        public static EventCondition Create(EventConditionSO eventConditionData)
        {
            Type[] argTypes = eventConditionData.eventRequired.ParametersTypes;
            Type genericType;
            switch (argTypes.Length)
            {
                case 0:
                    return new EventCondition(eventConditionData);

                case 1:
                    genericType = typeof(EventCondition<>).MakeGenericType(argTypes);
                    return Activator.CreateInstance(genericType, args: eventConditionData) as EventCondition;

                case 2:
                    genericType = typeof(EventCondition<,>).MakeGenericType(argTypes);
                    return Activator.CreateInstance(genericType, args: eventConditionData) as EventCondition;

                default:
                    return null;
            }

        }
    }
}
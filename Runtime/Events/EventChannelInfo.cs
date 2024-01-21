using System;
using System.Collections.Generic;

namespace Blackboard.Events
{
    [Serializable]
    public class EventChannelInfo
    {
        public string category;
        public Type channelType;
        public string channelName;
        public List<EventInfo> eventList;

        public EventChannelInfo(string category, List<EventInfo> eventList)
        {
            this.category = category;
            this.channelType = Type.GetType($"Blackboard.Events.{category}EventChannel, Mariosep.Blackboard");
            this.channelName = channelType?.Name;
            this.eventList = eventList;
        }
    }
}
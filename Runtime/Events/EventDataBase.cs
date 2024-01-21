using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;

namespace Blackboard.Events
{
    public class EventDataBase
    {
        private SerializedDictionary<string, EventChannelInfo> eventsDic;

        public SerializedDictionary<string, EventChannelInfo> EventsDic => eventsDic;
        public List<string> Categories => eventsDic.Keys.Select(key => key.ToString()).ToList();

        public int CategoriesCount => eventsDic.Count;
    
        public EventChannelInfo GetEventChannelFromCategory(string category)
        {
            return eventsDic.ContainsKey(category) ? eventsDic[category] : null;
        }
    
        public EventDataBase()
        {
            eventsDic = GenerateEventDataBase();
        }
    
        private SerializedDictionary<string, EventChannelInfo> GenerateEventDataBase()
        {
            IEnumerable<Type> GetAllEventTypes()
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(BaseEventSO)) && !type.IsAbstract);
            }
        
            var eventsByCategory = new Dictionary<string, List<EventInfo>>();
        
            var eventTypes = GetAllEventTypes();
            foreach (Type eventType in eventTypes)
            {
                var eventInfo = new EventInfo(eventType);

                string category = eventInfo.category;
            
                // Check if the category already exists in the dictionary
                if (!eventsByCategory.ContainsKey(category))
                {
                    // If not, add a new entry with an empty list
                    eventsByCategory[category] = new List<EventInfo>();
                }
            
                // Add the current EventField to the corresponding category in the dictionary
                eventsByCategory[category].Add(eventInfo);
            }

            var eventChannelByCategory = new SerializedDictionary<string, EventChannelInfo>();
            foreach ((string category, List<EventInfo> eventList) in eventsByCategory)
                eventChannelByCategory[category] = new EventChannelInfo(category, eventList);
        
            return eventChannelByCategory;
        }
    }
}
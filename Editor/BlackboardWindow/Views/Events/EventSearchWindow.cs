using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blackboard.Events;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Blackboard.Editor.Events
{
    public class EventSearchWindow
    {
        private static List<KeyValuePair<Type, string>> _eventPairs;

        public static void Open(Action<Type> callback)
        {
            var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);

            var actionPairs = GetEventPairs();

            var eventSearchProvider = ScriptableObject.CreateInstance<EventSearchProvider>();
            eventSearchProvider.Init(actionPairs, callback);

            SearchWindow.Open(new SearchWindowContext(mousePos), eventSearchProvider);
        }

        public static List<KeyValuePair<Type, string>> GetEventPairs()
        {
            IEnumerable<Type> GetAllEventTypes()
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(BaseEventSO)) && !type.IsAbstract);
            }

            _eventPairs = new List<KeyValuePair<Type, string>>();

            var eventTypes = GetAllEventTypes();

            foreach (Type eventType in eventTypes)
            {
                string eventName = eventType.Name.AddSpaceBeforeCapitalLetters();
                
                CategoryAttribute categoryAttribute = eventType.GetCustomAttribute<CategoryAttribute>();
                
                string eventPath = categoryAttribute != null
                    ? $"{categoryAttribute.Category}/{eventName}"
                    : eventName;
                
                var eventPair = new KeyValuePair<Type, string>(eventType, eventPath);
                _eventPairs.Add(eventPair);
            }

            return _eventPairs;
        }
    }
}
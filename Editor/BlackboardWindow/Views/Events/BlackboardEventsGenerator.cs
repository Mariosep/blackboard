using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Blackboard.Editor;
using UnityEditor;

namespace Blackboard.Events
{
// TODO: REFACTOR
    [InitializeOnLoad]
    public class BlackboardEventsGenerator
    {
        static BlackboardEventsGenerator()
        {
            var eventDataBase = BlackboardEditorManager.instance.EventDataBase;
            
            CreateAllEventChannelsScript(eventDataBase.EventsDic);
        }
        
        public static void CreateAllEventChannelsScript(SerializedDictionary<string, EventChannelInfo> eventsDic)
        {
            foreach ((string category, EventChannelInfo eventChannelInfo) in eventsDic)
                CreateEventChannelScript(category, eventChannelInfo);
        }

        private static void CreateEventChannelScript(string category, EventChannelInfo eventChannelInfo)
        {
            var eventFieldContentList = new List<string>();
        
            var channelDependencies = new HashSet<string>();
            
            foreach (EventInfo eventInfo in eventChannelInfo.eventList)
            {
                string eventFieldContent = "public Action";

                var eventArgs = eventInfo.parameters;

                if (eventArgs.Count > 0)
                {
                    string arg = eventArgs[0].type.Name;
                    eventFieldContent += $"<{arg}";
                    channelDependencies.Add(eventArgs[0].type.Namespace);
                    
                    for (var i = 1; i < eventArgs.Count; i++)
                    {
                        arg = eventArgs[i].type.Name;
                        eventFieldContent += $",{arg}";
                        channelDependencies.Add(eventArgs[i].type.Namespace);
                    }

                    eventFieldContent += ">";
                }

                eventFieldContent += $" {eventInfo.eventType.Name.ToCamelCase()};";
                eventFieldContentList.Add(eventFieldContent);
            }

            ScriptCreator.CreateScript("Assets/Blackboard/Scripts/Runtime/Events/Channels/", $"{category}EventChannel",
                eventFieldContentList, channelDependencies.ToList());
        }
    }
}
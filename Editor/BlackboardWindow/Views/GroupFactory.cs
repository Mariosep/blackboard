using Blackboard.Actors;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Editor
{
    public static class GroupFactory
    {
        public static ScriptableObject CreateGroup(string groupName, BlackboardElementType type, string id = null)
        {
            ScriptableObject group;
        
            switch (type)
            {
                case BlackboardElementType.Fact:
                    group = CreateFactGroup(groupName, id);
                    break;
                
                /*case BlackboardElementType.Event:
                    group = CreateEventGroup(groupName, id);
                    break;*/
            
                case BlackboardElementType.Actor:
                    group = CreateActorGroup(groupName, id);
                    break;

                case BlackboardElementType.Item:
                    group = CreateItemGroup(groupName, id);
                    break;
            
                default:
                    return null;
            }
        
            group.hideFlags = HideFlags.HideInHierarchy;

            return group;
        }
    
        public static FactGroupSO CreateFactGroup(string groupName, string id = null)
        {
            FactGroupSO factGroup = ScriptableObject.CreateInstance<FactGroupSO>();
        
            factGroup.groupName = groupName;
        
            if(id != null)
                factGroup.id = id;
            else
                factGroup.id = GUID.Generate().ToString();

            factGroup.name = $"factGroup-{factGroup.id}";
        
            return factGroup;
        }
    
        /*public static EventGroupSO CreateEventGroup(string groupName, string id = null)
        {
            EventGroupSO eventGroup = ScriptableObject.CreateInstance<EventGroupSO>();
        
            eventGroup.groupName = groupName;
        
            if(id != null)
                eventGroup.id = id;
            else
                eventGroup.id = GUID.Generate().ToString();
        
            eventGroup.name = $"eventGroup-{eventGroup.id}";
        
            return eventGroup;
        }*/
    
        public static ActorGroupSO CreateActorGroup(string groupName, string id = null)
        {
            ActorGroupSO actorGroup = ScriptableObject.CreateInstance<ActorGroupSO>();
        
            actorGroup.groupName = groupName;
        
            if(id != null)
                actorGroup.id = id;
            else
                actorGroup.id = GUID.Generate().ToString();
        
            actorGroup.name = $"actorGroup-{actorGroup.id}";
        
            return actorGroup;
        }
    
        public static ItemGroupSO CreateItemGroup(string groupName, string id = null)
        {
            ItemGroupSO itemGroup = ScriptableObject.CreateInstance<ItemGroupSO>();
        
            itemGroup.groupName = groupName;
        
            if(id != null)
                itemGroup.id = id;
            else
                itemGroup.id = GUID.Generate().ToString();
        
            itemGroup.name = $"itemGroup-{itemGroup.id}";
        
            return itemGroup;
        }
    }
}
using System;
using UnityEditor;
using UnityEngine;

public static class BlackboardElementFactory
{ 
    public static FactSO CreateFact(FactType type, string id = null)
    {
        FactSO fact = type switch
        {
            FactType.Bool => ScriptableObject.CreateInstance<BoolFactSO>(),
            FactType.Int => ScriptableObject.CreateInstance<IntFactSO>(),
            FactType.Float => ScriptableObject.CreateInstance<FloatFactSO>(),
            FactType.String => ScriptableObject.CreateInstance<StringFactSO>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        if(id != null)
            fact.id = id;
        else
            fact.id = GUID.Generate().ToString();
        
        fact.name = $"fact-{fact.id}";
        fact.blackboardElementType = BlackboardElementType.Fact;

        switch (type)
        {
            case FactType.Bool:
                fact.theName = "NewBool";
                break;
            
            case FactType.Float:
                fact.theName = "NewFloat";
                break;
            
            case FactType.Int:
                fact.theName = "NewInt";
                break;
            
            case FactType.String:
                fact.theName = "NewString";
                break;
        }
        
        fact.hideFlags = HideFlags.HideInHierarchy;
                
        return fact;
    }
    
    public static EventSO CreateEvent(BlackboardEventType type, string id = null)
    {
        EventSO eventSO = type switch
        {
            BlackboardEventType.Actor => ScriptableObject.CreateInstance<EventActorSO>(),
            BlackboardEventType.Item => ScriptableObject.CreateInstance<EventItemSO>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        if(id != null)
            eventSO.id = id;
        else
            eventSO.id = GUID.Generate().ToString();
        
        eventSO.name = $"event-{eventSO.id}";
        eventSO.blackboardElementType = BlackboardElementType.Event;
        eventSO.theName = "NewEvent";
        eventSO.hideFlags = HideFlags.HideInHierarchy;
        
        return eventSO;
    }
    
    public static ActorSO CreateActor(string id = null)
    {
        ActorSO actor = ScriptableObject.CreateInstance<ActorSO>();

        if(id != null)
            actor.id = id;
        else
            actor.id = GUID.Generate().ToString();
        
        actor.name = $"actor-{actor.id}";
        actor.blackboardElementType = BlackboardElementType.Actor;
        actor.theName = "NewActor";
        actor.hideFlags = HideFlags.HideInHierarchy;
        
        return actor;
    }
    
    public static ItemSO CreateItem(string id = null)
    {
        ItemSO item = ScriptableObject.CreateInstance<ItemSO>();

        if(id != null)
            item.id = id;
        else
            item.id = GUID.Generate().ToString();
        
        item.name = $"item-{item.id}";
        item.blackboardElementType = BlackboardElementType.Item;
        item.theName = "NewItem";
        item.hideFlags = HideFlags.HideInHierarchy;
        
        return item;
    }
}
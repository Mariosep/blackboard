using UnityEditor;
using UnityEngine;

public static class BlackboardFactory
{ 
    public static BlackboardSO CreateBlackboard(string name, string id = null)
    {
        BlackboardSO blackboard = ScriptableObject.CreateInstance<BlackboardSO>();
        
        blackboard.blackboardName = name;
        
        if(id != null)
            blackboard.id = id;
        else
            blackboard.id = GUID.Generate().ToString();
        
        // Create FactDataBase
        FactDataBaseSO factDataBase = DataBaseFactory.CreateFactDataBase();
        var factGroup = GroupFactory.CreateGroup("Facts", factDataBase.Type);
        factDataBase.AddGroup(factGroup);
        blackboard.factDataBase = factDataBase;

        // Create EventDataBase
        EventDataBaseSO eventDataBase = DataBaseFactory.CreateEventDataBase();
        var eventGroup = GroupFactory.CreateGroup("Events", eventDataBase.Type);
        eventDataBase.AddGroup(eventGroup);
        blackboard.eventDataBase = eventDataBase;

        // Create ActorDataBase
        ActorDataBaseSO actorDataBase = DataBaseFactory.CreateActorDataBase();
        var actorGroup = GroupFactory.CreateGroup("Actors", actorDataBase.Type);
        actorDataBase.AddGroup(actorGroup);
        blackboard.actorDataBase = actorDataBase;
        
        // Create ItemDataBase
        ItemDataBaseSO itemDataBase = DataBaseFactory.CreateItemDataBase();
        var itemGroup = GroupFactory.CreateGroup("Items", itemDataBase.Type);
        itemDataBase.AddGroup(itemGroup);
        blackboard.itemDataBase = itemDataBase;
        
        // Save blackboard
        ScriptableObjectUtility.SaveAsset(blackboard, $"Assets/SO/Blackboard/{blackboard.blackboardName}.asset");
        
        // Save databases
        ScriptableObjectUtility.SaveSubAsset(blackboard.factDataBase, blackboard);
        ScriptableObjectUtility.SaveSubAsset(blackboard.eventDataBase, blackboard);
        ScriptableObjectUtility.SaveSubAsset(blackboard.actorDataBase, blackboard);
        ScriptableObjectUtility.SaveSubAsset(blackboard.itemDataBase, blackboard);
        
        // Save default groups
        ScriptableObjectUtility.SaveSubAsset(factGroup, blackboard);
        ScriptableObjectUtility.SaveSubAsset(eventGroup, blackboard);
        ScriptableObjectUtility.SaveSubAsset(actorGroup, blackboard);
        ScriptableObjectUtility.SaveSubAsset(itemGroup, blackboard);
        
        return blackboard;
    }
}
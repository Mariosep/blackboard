using UnityEditor;

public class BlackboardManager : ScriptableSingleton<BlackboardManager>
{
    private BlackboardSO _blackboard;

    public BlackboardSO Blackboard
    {
        get
        {
            if (_blackboard == null)
            {
                string pathToAsset = AssetDataBaseExtensions.GetPathToScriptableObject<BlackboardSO>();
                _blackboard = AssetDatabase.LoadAssetAtPath<BlackboardSO>(pathToAsset);
            }
                
            return _blackboard;
        }

        set => _blackboard = value;
    }

    public FactDataBaseSO FactDataBase => Blackboard.factDataBase;
    public EventDataBaseSO EventDataBase => Blackboard.eventDataBase;
    public ActorDataBaseSO ActorDataBase => Blackboard.actorDataBase;
    public ItemDataBaseSO ItemDataBase => Blackboard.itemDataBase;
    
    /*public bool? GetBool(string categoryId, string factId)
    {
        if (BlackboardDataBase. TryGetBlackboard(categoryId, out BlackboardSO blackboard))
        {
            return blackboard.GetBool(factId);
        }

        return null;
    }*/

    public FactSO GetFact(string groupId, string factId)
    {
        if (FactDataBase.TryGetGroup(groupId, out FactGroupSO factGroup))
        {
            if (factGroup.TryGetElement(factId, out FactSO fact))
            {
                return fact;                                    
            }
        }

        return null;
    }

    public string GetFactPath(string groupId, string factId)
    {
        if (FactDataBase.TryGetGroup(groupId, out FactGroupSO factGroup))
        {
            if (factGroup.TryGetElement(factId, out FactSO fact))
            {
                return $"{factGroup.groupName}: {fact.theName}";                                    
            }
        }

        return "";
    }
    
    public EventSO GetEvent(string groupId, string eventId)
    {
        if (EventDataBase.TryGetGroup(groupId, out EventGroupSO eventGroup))
        {
            if (eventGroup.TryGetElement(eventId, out EventSO eventSo))
            {
                return eventSo;                                    
            }
        }

        return null;
    }
    
    public string GetEventPath(string groupId, string eventId)
    {
        if (EventDataBase.TryGetGroup(groupId, out EventGroupSO eventGroup))
        {
            if (eventGroup.TryGetElement(eventId, out EventSO eventSO))
            {
                return $"{eventGroup.groupName}: {eventSO.theName}";                                    
            }
        }

        return "";
    }
    
    public ActorSO GetActor(string groupId, string id)
    {
        if (ActorDataBase.TryGetGroup(groupId, out ActorGroupSO actorGroup))
        {
            if (actorGroup.TryGetElement(id, out ActorSO actor))
            {
                return actor;                                    
            }
        }

        return null;
    }

    public string GetActorPath(string groupId, string id)
    {
        if (ActorDataBase.TryGetGroup(groupId, out ActorGroupSO actorGroup))
        {
            if (actorGroup.TryGetElement(id, out ActorSO actor))
            {
                return $"{actorGroup.groupName}: {actor.theName}";                                    
            }
        }

        return "";
    }
    
    public ItemSO GetItem(string groupId, string id)
    {
        if (ItemDataBase.TryGetGroup(groupId, out ItemGroupSO itemGroup))
        {
            if (itemGroup.TryGetElement(id, out ItemSO item))
            {
                return item;                                    
            }
        }

        return null;
    }

    public string GetItemPath(string groupId, string id)
    {
        if (ItemDataBase.TryGetGroup(groupId, out ItemGroupSO itemGroup))
        {
            if (itemGroup.TryGetElement(id, out ItemSO item))
            {
                return $"{itemGroup.groupName}: {item.theName}";                                    
            }
        }

        return "";
    }
}
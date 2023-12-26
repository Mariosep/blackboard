using UnityEngine;

public class BlackboardManager : MonoBehaviour
{
    public BlackboardSO blackboardDatabase;
    
    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    public BoolFactSO GetBoolFact(string factName)
    {
        return blackboardDatabase.GetBoolFactByName(factName);
    }
    
    public IntFactSO GetIntFact(string factName)
    {
        return blackboardDatabase.GetIntFactByName(factName);
    }
    
    public FloatFactSO GetFloatFact(string factName)
    {
        return blackboardDatabase.GetFloatFactByName(factName);
    }
    
    public StringFactSO GetStringFact(string factName)
    {
        return blackboardDatabase.GetStringFactByName(factName);
    }
    
    public ActorSO GetActor(string actorName)
    {
        return blackboardDatabase.GetActorByName(actorName);
    }
    
    public ItemSO GetItem(string itemName)
    {
        return blackboardDatabase.GetItemByName(itemName);
    }
    
    public EventSO GetEvent(string eventName)
    {
        return blackboardDatabase.GetEventByName(eventName);
    }

    public EventItemSO GetEventItem(string eventName)
    {
        return blackboardDatabase.GetEventItemByName(eventName);
    }
    
    public EventActorSO GetEventActor(string eventName)
    {
        return blackboardDatabase.GetEventActorByName(eventName);
    }
}

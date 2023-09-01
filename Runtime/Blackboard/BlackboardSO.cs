using UnityEngine;

[CreateAssetMenu(fileName = "Blackboard", menuName = "Blackboard/Blackboard", order = 0)]
public class BlackboardSO : ScriptableObject
{
    public string id;
    public string blackboardName;
    
    public FactDataBaseSO factDataBase;
    public EventDataBaseSO eventDataBase;
    public ActorDataBaseSO actorDataBase;
    public ItemDataBaseSO itemDataBase;

    public bool BlackboardExists(string blackboardName)
    {
        return false;
    }
}
/*using UnityEngine;

[CreateAssetMenu(fileName = "BlackboardDataBase", menuName = "Blackboard/BlackboardDataBase", order = 0)]
public class BlackboardDataBaseSO : ScriptableObject
{
    public FactDataBaseSO factDataBase;
    public EventDataBaseSO eventDataBase;

    /*public void AddBlackboard(BlackboardSO blackboard)
    {
        blackboards.Add(blackboard);
        blackboardsDic.Add(blackboard.id, blackboard);
    }

    public void RemoveBlackboard(string id)
    {
        if (blackboardsDic.TryGetValue(id, out BlackboardSO blackboard))
        {
            blackboards.Remove(blackboard);
            blackboardsDic.Remove(id);
        }
        else
            throw new Exception("Can not remove blackboard because is not registered");
    }

    public bool CategoryExists(string category)
    {
        foreach (BlackboardSO blackboard in blackboards)
        {
            if (blackboard.category == category)
                return true;
        }

        return false;
    }

    public bool TryGetBlackboard(string id, out BlackboardSO blackboard)
    {
        if (blackboardsDic.TryGetValue(id, out blackboard))
            return true;
        else
            return false;
    }#1#
}*/
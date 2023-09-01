using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

public class ActorDataBaseSO : ScriptableObject, IDataBase
{
    public string id;
    
    public List<ActorGroupSO> groupsList = new List<ActorGroupSO>();
    
    [SerializedDictionary("ID", "FactGroup")]
    public SerializedDictionary<string, ActorGroupSO> groupsDic = new SerializedDictionary<string, ActorGroupSO>();

    public BlackboardElementType Type => BlackboardElementType.Actor;
    public int GroupListLenght => groupsList.Count;
    public List<ScriptableObject> GroupList => groupsList.Cast<ScriptableObject>().ToList();

    public void AddGroup(ScriptableObject group)
    {
        ActorGroupSO actorGroup = (ActorGroupSO)group;
        
        groupsList.Add(actorGroup);
        groupsDic.Add(actorGroup.id, actorGroup);
    }

    public void RemoveGroup(string id)
    {
        if (groupsDic.TryGetValue(id, out ActorGroupSO group))
        {
            groupsList.Remove(group);
            groupsDic.Remove(id);
        }
        else
            throw new Exception("Can not remove group because is not registered");
    }

    public ScriptableObject GetGroupById(string id)
    {
        return groupsDic[id];
    }

    public string GetIdByIndex(int index)
    {
        return groupsList[index].id;
    }

    public void Save()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    public bool GroupExists(string groupName)
    {
        foreach (ActorGroupSO group in groupsList)
        {
            if (group.groupName == groupName)
                return true;
        }

        return false;
    }

    public bool TryGetGroup(string id, out ActorGroupSO group)
    {
        if (groupsDic.TryGetValue(id, out group))
            return true;
        else
            return false;
    }

    public List<KeyValuePair<ActorSO, string>> GetPairs()
    {
        var pairs = new List<KeyValuePair<ActorSO, string>>();

        foreach (ActorGroupSO group in groupsList)
        {
            var currentPairs = group.GetPairs();
            
            if(currentPairs.Count > 0)
                pairs.AddRange(currentPairs);
        }
            
        return pairs;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

public class EventDataBaseSO : ScriptableObject, IDataBase
{
    public string id;
    
    public List<EventGroupSO> groupsList = new List<EventGroupSO>();
    
    [SerializedDictionary("ID", "FactGroup")]
    public SerializedDictionary<string, EventGroupSO> groupsDic = new SerializedDictionary<string, EventGroupSO>();
    
    public BlackboardElementType Type => BlackboardElementType.Event;
    public int GroupListLenght => groupsList.Count;
    public List<ScriptableObject> GroupList => groupsList.Cast<ScriptableObject>().ToList();
    
    public void AddGroup(ScriptableObject group)
    {
        EventGroupSO eventGroup = (EventGroupSO)group;
        
        groupsList.Add(eventGroup);
        groupsDic.Add(eventGroup.id, eventGroup);
    }
    
    public void RemoveGroup(string id)
    {
        if (groupsDic.TryGetValue(id, out EventGroupSO group))
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
    
    // TODO: Extract Save logic from ScriptableObject
    public void Save()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }
    
    
    public bool GroupExists(string groupName)
    {
        foreach (EventGroupSO group in groupsList)
        {
            if (group.groupName == groupName)
                return true;
        }

        return false;
    }

    public bool TryGetGroup(string id, out EventGroupSO group)
    {
        if (groupsDic.TryGetValue(id, out group))
            return true;
        else
            return false;
    }
    
    public List<KeyValuePair<EventSO, string>> GetPairs(EventType eventType)
    {
        var pairs = new List<KeyValuePair<EventSO, string>>();

        foreach (EventGroupSO group in groupsList)
        {
            var currentPairs = group.GetPairs(eventType);
            
            if(currentPairs.Count > 0)
                pairs.AddRange(currentPairs);
        }
            
        return pairs;
    }
    
    public List<KeyValuePair<EventSO, string>> GetPairs()
    {
        var pairs = new List<KeyValuePair<EventSO, string>>();

        foreach (EventGroupSO group in groupsList)
        {
            var currentPairs = group.GetPairs();
            
            if(currentPairs.Count > 0)
                pairs.AddRange(currentPairs);
        }
            
        return pairs;
    }
}
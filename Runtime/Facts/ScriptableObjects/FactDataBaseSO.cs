using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Facts
{
    public class FactDataBaseSO : ScriptableObject, IDataBase
    {
        public string id;
    
        public List<FactGroupSO> groupsList = new List<FactGroupSO>();
    
        [SerializedDictionary("ID", "FactGroup")]
        public SerializedDictionary<string, FactGroupSO> groupsDic = new SerializedDictionary<string, FactGroupSO>();
    
        public BlackboardElementType Type => BlackboardElementType.Fact;
        public int GroupListLength => groupsList.Count;
        public List<ScriptableObject> GroupList => groupsList.Cast<ScriptableObject>().ToList();
    
        public void AddGroup(ScriptableObject group)
        {
            FactGroupSO factGroup = (FactGroupSO)group;
        
            groupsList.Add(factGroup);
            groupsDic.Add(factGroup.id, factGroup);
        }

        public void RemoveGroup(string id)
        {
            if (groupsDic.TryGetValue(id, out FactGroupSO group))
            {
                groupsList.Remove(group);
                groupsDic.Remove(id);
            }
            else
                throw new Exception("Can not remove group because is not registered");
        }
    
        public ElementGroupSO GetGroupById(string id)
        {
            return groupsDic[id];
        }

        public string GetIdByIndex(int index)
        {
            return groupsList[index].id;
        }

        public void Save()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
#endif
        }

        public void SaveState()
        {
            foreach (var group in groupsList)
            {
                group.SaveState();
            }
        }
    
        public void RevertChanges()
        {
            foreach (var group in groupsList)
            {
                group.RevertChanges();
            }
        }

        public bool GroupExists(string groupName)
        {
            foreach (FactGroupSO group in groupsList)
            {
                if (group.groupName == groupName)
                    return true;
            }

            return false;
        }

        public bool TryGetGroup(string id, out FactGroupSO group)
        {
            if (groupsDic.TryGetValue(id, out group))
                return true;
            else
                return false;
        }

        #region GetPairs
        public List<KeyValuePair<FactSO, string>> GetPairs(FactType factType)
        {
            var pairs = new List<KeyValuePair<FactSO, string>>();

            foreach (FactGroupSO group in groupsList)
            {
                var currentPairs = group.GetPairs(factType);
            
                if(currentPairs.Count > 0)
                    pairs.AddRange(currentPairs);
            }
            
            return pairs;
        }
    
        public List<KeyValuePair<FactSO, string>> GetPairs()
        {
            var pairs = new List<KeyValuePair<FactSO, string>>();

            foreach (FactGroupSO group in groupsList)
            {
                var currentPairs = group.GetPairs();
            
                if(currentPairs.Count > 0)
                    pairs.AddRange(currentPairs);
            }
            
            return pairs;
        }
        #endregion
    }
}
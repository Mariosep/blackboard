using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Items
{
    public class ItemDataBaseSO : ScriptableObject, IDataBase
    {
        public string id;
    
        public List<ItemGroupSO> groupsList = new List<ItemGroupSO>();
    
        [SerializedDictionary("ID", "FactGroup")]
        public SerializedDictionary<string, ItemGroupSO> groupsDic = new SerializedDictionary<string, ItemGroupSO>();

        public BlackboardElementType Type => BlackboardElementType.Item;
        public int GroupListLength => groupsList.Count;
        public List<ScriptableObject> GroupList => groupsList.Cast<ScriptableObject>().ToList();

        public void AddGroup(ScriptableObject group)
        {
            ItemGroupSO itemGroup = (ItemGroupSO)group;
        
            groupsList.Add(itemGroup);
            groupsDic.Add(itemGroup.id, itemGroup);
        }

        public void RemoveGroup(string id)
        {
            if (groupsDic.TryGetValue(id, out ItemGroupSO group))
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
            foreach (ItemGroupSO group in groupsList)
            {
                if (group.groupName == groupName)
                    return true;
            }

            return false;
        }

        public bool TryGetGroup(string id, out ItemGroupSO group)
        {
            if (groupsDic.TryGetValue(id, out group))
                return true;
            else
                return false;
        }

        public List<KeyValuePair<ItemSO, string>> GetPairs()
        {
            var pairs = new List<KeyValuePair<ItemSO, string>>();

            foreach (ItemGroupSO group in groupsList)
            {
                var currentPairs = group.GetPairs();
            
                if(currentPairs.Count > 0)
                    pairs.AddRange(currentPairs);
            }
            
            return pairs;
        }
    }
}
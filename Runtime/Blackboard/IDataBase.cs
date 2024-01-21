using System.Collections.Generic;
using UnityEngine;

namespace Blackboard
{
    public interface IDataBase
    {
        public BlackboardElementType Type { get; }
        public int GroupListLength { get; }
        public List<ScriptableObject> GroupList { get; }

        public void AddGroup(ScriptableObject group);
        public void RemoveGroup(string id);
        public ElementGroupSO GetGroupById(string id);
        public string GetIdByIndex(int index);

        public void Save();
    
        public void SaveState();
        public void RevertChanges();
    }
}
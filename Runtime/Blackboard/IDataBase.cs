using System.Collections.Generic;
using UnityEngine;

public interface IDataBase
{
    public BlackboardElementType Type { get; }
    public int GroupListLenght { get; }
    public List<ScriptableObject> GroupList { get; }

    public void AddGroup(ScriptableObject group);
    public void RemoveGroup(string id);
    public ScriptableObject GetGroupById(string id);
    public string GetIdByIndex(int index);

    public void Save();
}
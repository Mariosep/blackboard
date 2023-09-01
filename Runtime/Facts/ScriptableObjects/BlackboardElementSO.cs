using System;
using UnityEngine;

public abstract class BlackboardElementSO : ScriptableObject
{
    public Action onNameChanged;
    
    public string id;
    public string groupId;
    public string theName;
    public string description;

    public BlackboardElementType blackboardElementType;

    public void SetName(string newName)
    {
        theName = newName;
        
        onNameChanged?.Invoke();
    }
    
}

public enum BlackboardElementType
{
    Fact,
    Event,
    Actor,
    Item
}
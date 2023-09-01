using System;
using UnityEngine;

[Serializable]
public class Actor
{
    public string id;
    public string categoryId;
    
    [SerializeField] protected ActorSO actorData;
}
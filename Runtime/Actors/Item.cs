using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string id;
    public string categoryId;
    
    [SerializeField] protected ItemSO itemData;
}
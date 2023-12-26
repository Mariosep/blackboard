using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSO> _itemsList = new List<ItemSO>();

    private InteractionChannel interactionChannel;

    public List<ItemSO> ItemsList => _itemsList;

    private void Awake()
    {
        ServiceLocator.Register<Inventory>(this);
    }

    private void Start()
    {
        interactionChannel = ServiceLocator.Get<InteractionChannel>();
        interactionChannel.onItemGathered += AddItem;
    }

    private void AddItem(ItemSO item)
    {
        _itemsList.Add(item);
    }

    private void RemoveItem(ItemSO item)
    {
        _itemsList.Remove(item);
    }

    private void OnDestroy()
    {
        interactionChannel.onItemGathered -= AddItem;
    }
}

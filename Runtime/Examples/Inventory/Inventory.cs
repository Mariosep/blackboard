using System.Collections.Generic;
using Blackboard.Events;
using Blackboard.Items;
using UnityEngine;

namespace Blackboard.Examples
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> _itemsList = new List<ItemSO>();

        private InteractionEventChannel interactionChannel;

        public List<ItemSO> ItemsList => _itemsList;

        private void Awake()
        {
            ServiceLocator.Register<Inventory>(this);
        }

        private void Start()
        {
            interactionChannel = ServiceLocator.Get<InteractionEventChannel>();
            //interactionChannel.onItemGathered += AddItem;
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
            //if(interactionChannel != null)
                //interactionChannel.onItemGathered -= AddItem;
        }
    }
    
}
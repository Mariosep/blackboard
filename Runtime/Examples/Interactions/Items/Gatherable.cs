using Blackboard.Events;
using Blackboard.Items;
using UnityEngine;

namespace Blackboard.Interactions
{
    [RequireComponent(typeof(Item))]
    public class Gatherable : MonoBehaviour, IInteractable
    {
        private Item item;

        public string Name => item.Name;
        public string InteractionName => "Take";

        public bool InteractionEnabled => true;

        private InteractionEventChannel interactionEventChannel;

        private void Awake()
        {
            item = GetComponent<Item>();
        }

        private void Start()
        {
            interactionEventChannel = ServiceLocator.Get<InteractionEventChannel>();
        }

        public bool CanInteract()
        {
            return InteractionEnabled;
        }

        public void Interact()
        {
            print($"{Name} gathered.");

            gameObject.SetActive(false);
            
            interactionEventChannel.onItemGathered?.Invoke(item.Data);
        }
    }
}
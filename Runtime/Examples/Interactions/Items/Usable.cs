using Blackboard.Commands;
using Blackboard.Events;
using Blackboard.Interactions;
using Blackboard.Items;
using Blackboard.Requirement;
using UnityEngine;

namespace Blackboard.Examples
{
    [RequireComponent(typeof(Item))]
    public class Usable : MonoBehaviour, IInteractable
    {
        private Item item;
    
        public bool hasRequirements;
        public RequirementsSO requirementsToUse;
        private Requirements req;
    
        public CommandList onUse;

        private bool interactionEnabled; 
        private InteractionEventChannel interactionChannel;
        private Inventory inventory;
    
        public string Name => item.Name;
        public string InteractionName => "Use";
        public bool InteractionEnabled => interactionEnabled;
    
        private void Awake()
        {
            item = GetComponent<Item>();
            interactionEnabled = true;

            if(hasRequirements)
                req = new Requirements(requirementsToUse);
        }
    
        private void Start()
        {
            interactionChannel = ServiceLocator.Get<InteractionEventChannel>();
            inventory = ServiceLocator.Get<Inventory>();
        }
    
        public bool CanInteract()
        {
            return InteractionEnabled && (!hasRequirements || req.AreFulfilled);
        }
    
        public void Interact()
        {
            print($"{Name} used.");

            interactionEnabled = false;
        
            interactionChannel.onItemUsed?.Invoke(item.itemData);
            onUse.Execute();
        }
    }
}
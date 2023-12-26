using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Item))]
public class Usable : MonoBehaviour, IInteractable
{
    private Item item;
    
    public EventItemSO useEvent;
    public ItemSO itemRequired;
    public UnityEvent onUse;

    private bool interactionEnabled; 
    private InteractionChannel interactionChannel;
    private Inventory inventory;
    
    public string Name => item.Name;
    public string InteractionName => "Use";
    public bool InteractionEnabled => interactionEnabled;
    
    private void Awake()
    {
        item = GetComponent<Item>();
        interactionEnabled = true;
    }
    
    private void Start()
    {
        interactionChannel = ServiceLocator.Get<InteractionChannel>();
        inventory = ServiceLocator.Get<Inventory>();
    }
    
    public bool CanInteract()
    {
        return InteractionEnabled && itemRequired != null && inventory.ItemsList.Contains(itemRequired);
    }
    
    public void Interact()
    {
        print($"{Name} used.");

        interactionEnabled = false;
        
        useEvent.Invoke(item.Data);
        onUse.Invoke();
    }
}
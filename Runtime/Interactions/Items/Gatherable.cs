using UnityEngine;

[RequireComponent(typeof(Item))]
public class Gatherable : MonoBehaviour, IInteractable
{
    private Item item;

    public EventItemSO gatherEvent;
    
    public string Name => item.Name;
    public string InteractionName => "Take";

    public bool InteractionEnabled => true;

    private InteractionChannel interactionChannel;
    private InteractionManager interactionManager;
    
    private void Awake()
    {
        item = GetComponent<Item>();
    }

    private void Start()
    {
        interactionChannel = ServiceLocator.Get<InteractionChannel>();
        interactionManager = ServiceLocator.Get<InteractionManager>();

        //gatherEvent = (EventItemSO) interactionManager.GetInteractionEvent(this);
    }

    public bool CanInteract()
    {
        return InteractionEnabled;
    }

    public void Interact()
    {
        print($"{Name} gathered.");

        gameObject.SetActive(false);
        
        //gatherEvent.Invoke(item.Data);
        interactionChannel.onItemGathered?.Invoke(item.Data);
    }
}
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public InteractionsSO interactions;
    
    private void Awake()
    {
        ServiceLocator.Register(this);
    }

    public EventSO GetInteractionEvent(IInteractable interactable)
    {
        string interactableName = interactable.GetType().Name;
        
        foreach (InteractionEventTrigger interaction in interactions.interactions)
        {
            if (interaction.interactableName == interactableName)
            {
                return interaction.eventToTrigger;
            }
        }

        return null;
    }
}

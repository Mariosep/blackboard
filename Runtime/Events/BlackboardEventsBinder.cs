using UnityEngine;

public class BlackboardEventsBinder : MonoBehaviour
{
    private BlackboardManager blackboardManager;
    private InteractionChannel interactionChannel;

    private void Start()
    {
        blackboardManager = ServiceLocator.Get<BlackboardManager>();
        interactionChannel = ServiceLocator.Get<InteractionChannel>();
        
        BindEvents();
    }

    private void BindEvents()
    {
        EventItemSO gatherEvent = blackboardManager.GetEventItem("Gather");
        interactionChannel.onItemGathered += e => gatherEvent.Invoke(e);
    }
}

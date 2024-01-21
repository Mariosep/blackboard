using System;
using Blackboard.Interactions;
using Blackboard.Items;

namespace Blackboard.Events
{
    public class InteractionEventChannel : EventChannel
    {
	 	public Action<string> onDoorOpened;
	 	public Action<IInteractable> onInteract;
	 	public Action<IInteractable> onInteractableTriggerEnter;
	 	public Action<IInteractable> onInteractableTriggerExit;
	 	public Action<ItemSO> onItemGathered;
	 	public Action<ItemSO> onItemUsed;
    }
}

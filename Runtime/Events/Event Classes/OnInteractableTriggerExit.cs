using Blackboard;
using Blackboard.Events;
using Blackboard.Interactions;

[Category("Interaction")]
[Description("Invoked when interactable trigger exit.")]
public class OnInteractableTriggerExit : EventSO<IInteractable>
{
    [Parameter] public IInteractable interactable;
}
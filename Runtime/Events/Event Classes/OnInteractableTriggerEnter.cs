using Blackboard;
using Blackboard.Events;
using Blackboard.Interactions;

[Category("Interaction")]
[Description("Invoked when interactable trigger enter.")]
public class OnInteractableTriggerEnter : EventSO<IInteractable>
{
    [Parameter] public IInteractable interactable;
}
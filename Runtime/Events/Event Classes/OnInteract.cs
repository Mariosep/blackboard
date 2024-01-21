using Blackboard;
using Blackboard.Events;
using Blackboard.Interactions;

[Category("Interaction")]
[Description("Invoked on interactaction performed.")]
public class OnInteract : EventSO<IInteractable>
{
    [Parameter] public IInteractable interactable;
}
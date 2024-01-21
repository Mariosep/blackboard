using Blackboard;
using Blackboard.Events;
using Blackboard.Items;

[Category("Interaction")]
[Description("Invoked on interactable used.")]
public class OnItemUsed : EventSO<ItemSO>
{
    [Parameter] public ItemSO itemData;
}
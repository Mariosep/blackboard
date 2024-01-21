using Blackboard;
using Blackboard.Events;
using Blackboard.Items;

[Category("Interaction")]
[Description("Invoked when item gathered.")]
public class OnItemGathered : EventSO<ItemSO>
{
    [Parameter] public ItemSO itemGathered;
}
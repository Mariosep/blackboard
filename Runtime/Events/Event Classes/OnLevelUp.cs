using Blackboard;
using Blackboard.Events;

[Category("General")]
[Description("Invoked when the player's level increases.")]
public class OnLevelUp : EventSO<int>
{
    [Parameter] public int newLevel;
}
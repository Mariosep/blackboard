using Blackboard;
using Blackboard.Events;

[Category("Interaction")]
[Description("Invoked when a door is open.")]
public class OnDoorOpened : EventSO<string>
{
    [Parameter] public string doorName;
}
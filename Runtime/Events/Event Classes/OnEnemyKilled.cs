using Blackboard;
using Blackboard.Events;

[Category("Combat")]
[Description("Invoked when a enemy is killed.")]
public class OnEnemyKilled : EventSO<string>
{
    [Parameter] public string enemyName;
}
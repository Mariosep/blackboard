using Blackboard;
using Blackboard.Events;

[Category("Combat")]
[Description("Invoked when a enemy is spawned.")]
public class OnEnemySpawned : EventSO<string>
{
    [Parameter] public string enemyName;
}
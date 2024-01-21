using System;

namespace Blackboard.Events
{
    public class CombatEventChannel : EventChannel
    {
	 	public Action<string> onEnemyKilled;
	 	public Action<string> onEnemySpawned;
    }
}

using System;

namespace Blackboard.Events
{
    public class GeneralEventChannel : EventChannel
    {
	 	public Action onLevelStarted;
	 	public Action<int> onLevelUp;
	 	public Action<int> onScoreIncreased;
	 	public Action<int> onGamePaused;
    }
}

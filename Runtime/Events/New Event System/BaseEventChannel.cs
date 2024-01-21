using System;

namespace Blackboard.Events
{
	public class BaseEventChannel : EventChannel
	{
		public Action<string,int> onItemGathered;
		public Action<int> onScoreIncreased;
	}
}
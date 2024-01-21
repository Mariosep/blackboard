using Blackboard;
using Blackboard.Events;

[Category("General")]
[Description("Invoked when score is increased.")]
public class OnScoreIncreased : EventSO<int>
{
    [Parameter] public int scoreFact;
}

[Category("General")]
[Description("Invoked when score is increased.")]
public class OnGamePaused : EventSO<int>
{
    [Parameter] public int timeToPause;
}
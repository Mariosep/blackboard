using System;

public abstract class Condition
{
    public System.Action onGoalCompleted;
    public abstract bool CheckConditionGoal();
}
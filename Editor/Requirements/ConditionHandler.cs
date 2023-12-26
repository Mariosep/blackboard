using System;

public abstract class ConditionHandler
{
    public System.Action onValueTypeChanged;
    
    public ConditionType type;

    public abstract void SetElement(BlackboardElementSO elementSelected);
}
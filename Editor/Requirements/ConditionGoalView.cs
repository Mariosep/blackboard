using UnityEngine.UIElements;

public abstract class ConditionGoalView : VisualElement
{
    public abstract void BindCondition(ConditionSO condition);
}
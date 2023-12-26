using UnityEditor;
using UnityEngine;

public static class ConditionSOFactory
{
    public static ConditionSO CreateCondition(ConditionType type)
    {
        switch (type)
        {
            case ConditionType.Fact:
                var factCondition = ScriptableObject.CreateInstance<FactConditionSO>();
                factCondition.Init(GUID.Generate().ToString());

                return factCondition;
            
            case ConditionType.Event:
                var eventCondition = ScriptableObject.CreateInstance<EventConditionSO>();
                eventCondition.Init(GUID.Generate().ToString());

                return eventCondition;
            
            default:
                return null;
        }
    }
    
    public static ConditionSO CreateCondition(BlackboardElementSO blackboardElement)
    {
        switch (blackboardElement.blackboardElementType)
        {
            case BlackboardElementType.Fact:
                var factCondition = ScriptableObject.CreateInstance<FactConditionSO>();
                factCondition.Init(GUID.Generate().ToString());
                factCondition.SetElementRequired(blackboardElement);

                return factCondition;
            
            case BlackboardElementType.Event:
                var eventCondition = ScriptableObject.CreateInstance<EventConditionSO>();
                eventCondition.Init(GUID.Generate().ToString());
                eventCondition.SetElementRequired(blackboardElement);

                return eventCondition;
            
            default:
                return null;
        }
    }
}
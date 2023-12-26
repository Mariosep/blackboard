using System;
using System.Collections.Generic;

public class FactConditionSO : ConditionSO
{
    public FactSO fact;

    public bool boolRequired;
    public int intRequired;
    public float floatRequired;
    public string stringRequired;

    public OperatorType comparisonOperator;
    
    public override void Init(string id)
    {
        base.Init(id);

        type = ConditionType.Fact;
    }
    
    public override void SetElementRequired(BlackboardElementSO elementRequired)
    {
        if (elementRequired is FactSO factRequirement)
        {
            fact = factRequirement;
        }
    }

    public override BlackboardElementSO GetElementRequired()
    {
        return fact;
    }

    public List<OperatorType> GetAvailableOperators()
    {
        switch (fact.type)
        {
            case FactType.Bool:
                return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual };
            
            case FactType.Int:
                return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual, OperatorType.Less, OperatorType.LessOrEqual, OperatorType.Greather, OperatorType.GreatherOrEqual };
            
            case FactType.Float:
                return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual, OperatorType.Less, OperatorType.LessOrEqual, OperatorType.Greather, OperatorType.GreatherOrEqual };
            
            case FactType.String:
                return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual };
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
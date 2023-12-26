public class FactCondition : Condition
{
    private FactConditionSO conditionData;
    
    public FactCondition(FactConditionSO conditionData)
    {
        this.conditionData = conditionData;
    }
    
    public override bool CheckConditionGoal()
    {
        return conditionData.fact switch
        {
            BoolFactSO boolFact => ComparisonUtility.Compare(boolFact.Value, conditionData.boolRequired, conditionData.comparisonOperator),
            IntFactSO intFact => ComparisonUtility.Compare(intFact.Value, conditionData.intRequired, conditionData.comparisonOperator),
            FloatFactSO floatFact => ComparisonUtility.Compare(floatFact.Value, conditionData.floatRequired, conditionData.comparisonOperator),
            StringFactSO stringFact => ComparisonUtility.Compare(stringFact.Value, conditionData.stringRequired, conditionData.comparisonOperator),
            _ => false
        };
    }
}
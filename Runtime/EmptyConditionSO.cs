public class EmptyConditionSO : ConditionSO
{
    public override void SetElementRequired(BlackboardElementSO elementRequired){}
    public override BlackboardElementSO GetElementRequired() => null;
}
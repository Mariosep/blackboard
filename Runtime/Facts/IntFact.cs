using System;

[Serializable]
public class IntFact : Fact<IntFactSO, int>
{
    public int Value
    {
        get => fact.Value;
        set => fact.Value = value;
    }
    
    public IntFact()
    {
        factType = FactType.Int;
    }
    
    public override void Setup()
    {
        if (HasValue)
        {
            fact.onValueChanged -= OnValueChanged;
            fact.onValueChanged += OnValueChanged;

            FactInitializer.onExit -= OnDestroy;
            FactInitializer.onExit += OnDestroy;
        }
    }
    
    public void SetFact(IntFactSO fact)
    {
        if (this.fact != null)
        {
            this.fact.onValueChanged -= OnValueChanged;
        }

        this.fact = fact;
        this.fact.onValueChanged += OnValueChanged;
    }

    public override void OnDestroy()
    {
        if(HasValue)
        {
            fact.onValueChanged -= OnValueChanged;
            FactInitializer.onExit -= OnDestroy;
        }
    }
}
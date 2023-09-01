using System;

[Serializable]
public class BoolFact : Fact<BoolFactSO, bool>
{
    public bool Value
    {
        get => fact.Value;
        set => fact.Value = value;
    }
    public BoolFact()
    {
        factType = FactType.Bool;
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
    
    public void SetFact(BoolFactSO fact)
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

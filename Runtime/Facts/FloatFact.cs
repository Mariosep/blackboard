using System;

[Serializable]
public class FloatFact : Fact<FloatFactSO, float>
{
    public float Value
    {
        get => fact.Value;
        set => fact.Value = value;
    }
    
    public FloatFact()
    {
        factType = FactType.Float;
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
    
    public void SetFact(FloatFactSO fact)
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
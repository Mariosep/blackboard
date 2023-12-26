using System;

public static class FactViewFactory
{
    public static FactView CreateFactView(Type factType, bool showValue = false)
    {
        if (factType == typeof(BoolFactSO))
        {
            return new BoolFactView(showValue);
        }
        else if (factType == typeof(IntFactSO))
        {
            return new IntFactView(showValue);
        }
        else if (factType == typeof(FloatFactSO))
        {
            return new FloatFactView(showValue);
        }
        else if (factType == typeof(StringFactSO))
        {
            return new StringFactView(showValue);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(factType));
        }
    }
}
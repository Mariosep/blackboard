using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatFactSO", menuName = "Blackboard/Facts/Float")]
public class FloatFactSO : FactSO
{
    public Action<float> onValueChanged;
    
    [SerializeField] private float _value;
    
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            onValueChanged?.Invoke(value);
        }
    }
    
    public override void OnEnable()
    {
        base.OnEnable();
        type = FactType.Float;
    }
}
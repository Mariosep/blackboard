using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StringFactSO", menuName = "Blackboard/Facts/String")]
public class StringFactSO : FactSO
{
    public Action<string> onValueChanged;

    [SerializeField] private string _value;
    
    public string Value
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
        type = FactType.String;
    }
}
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolFactSO", menuName = "Blackboard/Facts/Bool")]
public class BoolFactSO : FactSO
{
    public Action<bool> onValueChanged;
    
    [SerializeField] private bool _value;

    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            onValueChanged?.Invoke(value);
        }
    }

    private void OnEnable()
    {
        type = FactType.Bool;
    }
}
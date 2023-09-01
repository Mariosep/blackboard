using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StringFactSO", menuName = "Blackboard/Facts/String")]
public class StringFactSO : FactSO
{
    public Action<string> onValueChanged;

    public int hola;
    
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
    
    private void OnEnable()
    {
        type = FactType.String;
    }
}
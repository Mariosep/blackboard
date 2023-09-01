using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntFactSO", menuName = "Blackboard/Facts/Int")]
public class IntFactSO : FactSO
{
    public Action<int> onValueChanged;
    
    [SerializeField] private int _value;
    
    public int Value
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
        type = FactType.Int;
    }
}
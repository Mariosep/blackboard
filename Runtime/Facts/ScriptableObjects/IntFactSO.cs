using System;
using Unity.Properties;
using UnityEngine;

namespace Blackboard.Facts
{
    [CreateAssetMenu(fileName = "IntFactSO", menuName = "Blackboard/Facts/Int")]
    public class IntFactSO : FactSO
    {
        public Action<int> onValueChanged;
    
        [SerializeField, DontCreateProperty] private int _value;
    
        [CreateProperty]
        public int Value
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
            type = FactType.Int;
        }
    }
}
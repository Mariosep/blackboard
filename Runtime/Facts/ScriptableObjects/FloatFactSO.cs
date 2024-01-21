using System;
using Unity.Properties;
using UnityEngine;

namespace Blackboard.Facts
{
    [CreateAssetMenu(fileName = "FloatFactSO", menuName = "Blackboard/Facts/Float")]
    public class FloatFactSO : FactSO
    {
        public Action<float> onValueChanged;
    
        [SerializeField, DontCreateProperty] private float _value;
    
        [CreateProperty]
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
}
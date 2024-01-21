using System;
using Unity.Properties;
using UnityEngine;

namespace Blackboard.Facts
{
    [CreateAssetMenu(fileName = "BoolFactSO", menuName = "Blackboard/Facts/Bool")]
    public class BoolFactSO : FactSO
    {
        public Action<bool> onValueChanged;
    
        [SerializeField, DontCreateProperty] private bool _value;

        [CreateProperty]
        public bool Value
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
            type = FactType.Bool;
        }
    }
}
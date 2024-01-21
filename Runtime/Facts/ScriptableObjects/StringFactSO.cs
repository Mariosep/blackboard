using System;
using Unity.Properties;
using UnityEngine;

namespace Blackboard.Facts
{
    [CreateAssetMenu(fileName = "StringFactSO", menuName = "Blackboard/Facts/String")]
    public class StringFactSO : FactSO
    {
        public Action<string> onValueChanged;

        [SerializeField, DontCreateProperty] private string _value;
    
        [CreateProperty]
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
}
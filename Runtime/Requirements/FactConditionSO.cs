using System;
using System.Collections.Generic;
using Blackboard.Facts;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Requirement
{
    public class FactConditionSO : ConditionSO
    {
        public FactSO fact;

        // TODO: Refactor
        [SerializeField] private bool boolRequired;
        [SerializeField] private int intRequired;
        [SerializeField] private float floatRequired;
        [SerializeField] private string stringRequired;

        public OperatorType comparisonOperator;

        public bool BoolRequired
        {
            get => boolRequired;
            set
            {
                boolRequired = value;
                onConditionModified?.Invoke();
            } 
        }
        
        public int IntRequired
        {
            get => intRequired;
            set
            {
                intRequired = value;
                onConditionModified?.Invoke();
            } 
        }
        
        public float FloatRequired
        {
            get => floatRequired;
            set
            {
                floatRequired = value;
                onConditionModified?.Invoke();
            } 
        }
        
        public string StringRequired
        {
            get => stringRequired;
            set
            {
                stringRequired = value;
                onConditionModified?.Invoke();
            } 
        }
        
        public override void Init(string id)
        {
            base.Init(id);

            type = ConditionType.Fact;
        }
    
        public override void SetElementRequired(BlackboardElementSO elementRequired)
        {
            if (elementRequired is FactSO factRequirement)
            {
                fact = factRequirement;
            }
        }
        
        public override BlackboardElementSO GetElementRequired()
        {
            return fact;
        }

        public void SetComparisonOperator(OperatorType operatorType)
        {
            this.comparisonOperator = operatorType;
            
            onConditionModified?.Invoke();
        }
        
        public List<OperatorType> GetAvailableOperators()
        {
            switch (fact.type)
            {
                case FactType.Bool:
                    return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual };
            
                case FactType.Int:
                    return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual, OperatorType.Less, OperatorType.LessOrEqual, OperatorType.Greather, OperatorType.GreatherOrEqual };
            
                case FactType.Float:
                    return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual, OperatorType.Less, OperatorType.LessOrEqual, OperatorType.Greather, OperatorType.GreatherOrEqual };
            
                case FactType.String:
                    return new List<OperatorType>() { OperatorType.Equal, OperatorType.NotEqual };
            
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            int rightPadding = Math.Max(30 - fact.Name.Length, 0);  
            
            string factString = fact.Name.PadRight(rightPadding);

            string operatorString = ComparisonUtility.OperatorTypeToString(comparisonOperator);

            factString += $" {operatorString.PadRight(8 - operatorString.Length)}";
            
            switch (fact.type)
            {
                case FactType.Bool:
                    factString += boolRequired.ToString();
                    break;
                case FactType.Int:
                    factString += intRequired.ToString();
                    break;
                case FactType.Float:
                    factString += floatRequired.ToString();
                    break;
                case FactType.String:
                    factString += stringRequired;
                    break;
            }

            return factString;
        }

        public override void SaveAs(ScriptableObject mainAsset)
        {
#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(this, mainAsset);
            AssetDatabase.SaveAssets();
#endif
        }
    }
}
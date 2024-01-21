using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blackboard.Requirement;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class ConditionGoalFactValueView : ConditionGoalView
    {
        private readonly string uxmlName = "UXML/RequirementGoalFactValue.uxml";

        private VisualElement conditionGoalRoot;
        private DropdownField comparisonOperatorDropdown;
    
        private FactConditionSO factCondition;
    
        public ConditionGoalFactValueView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            conditionGoalRoot = this.Q<VisualElement>("requirement-goal");
            comparisonOperatorDropdown = this.Q<DropdownField>("comparison-operator");
        
            comparisonOperatorDropdown.RegisterValueChangedCallback(e =>
            {
                factCondition.SetComparisonOperator((OperatorType) comparisonOperatorDropdown.index);
            });
        }

        public override void BindCondition(ConditionSO condition)
        {
            factCondition = (FactConditionSO) condition;

            int previousIndex = (int)factCondition.comparisonOperator;
        
            List<OperatorType> availableOperators = factCondition.GetAvailableOperators();
            List<string> availableOperatorsString = availableOperators.Select(ComparisonUtility.OperatorTypeToString).ToList();
            comparisonOperatorDropdown.choices = availableOperatorsString;

            if (previousIndex < availableOperators.Count)
                comparisonOperatorDropdown.index = previousIndex;
            else
                comparisonOperatorDropdown.index = 0;

            var factValueField = ConditionGoalViewFactory.CreateGoalFactValueField(factCondition);
            conditionGoalRoot.Add(factValueField);
        }
    }
}
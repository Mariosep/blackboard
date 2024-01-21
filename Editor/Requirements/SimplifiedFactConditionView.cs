using System.IO;
using Blackboard.Facts;
using Blackboard.Requirement;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class SimplifiedFactConditionView : SimplifiedConditionView
    {
        private readonly string uxmlName = "UXML/SimplifiedFactCondition.uxml";
        
        private FactConditionSO conditionData;
        
        private Label factLabel;
        private Label comparisonOperatorLabel;
        private Label valueLabel;
        
        public SimplifiedFactConditionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            fulfilledToggle = this.Q<Toggle>();
            factLabel = this.Q<Label>("fact__label");
            comparisonOperatorLabel = this.Q<Label>("comparison-operator__label");
            valueLabel = this.Q<Label>("value__label");
        }

        public override void BindCondition(SimplifiedRequirementsListView requirementsListView, ConditionSO condition)
        {
            this.requirementsListView = requirementsListView;
            this.conditionData = (FactConditionSO)condition;
            
            condition.onConditionModified += UpdateContent;
            this.requirementsListView.onMinFactLabelWidthModified += UpdateMinFactLabelWidth;
            factLabel.RegisterCallback<GeometryChangedEvent>(_ => OnLabelStyleResolved());

            UpdateContent();
        }

        protected override void UpdateContent()
        {
            factLabel.text = conditionData.fact.Name;
            comparisonOperatorLabel.text = ComparisonUtility.OperatorTypeToString(conditionData.comparisonOperator);
            
            switch (conditionData.fact.type)
            {
                case FactType.Bool:
                    valueLabel.text = conditionData.BoolRequired.ToString();
                    break;
                case FactType.Int:
                    valueLabel.text = conditionData.IntRequired.ToString();
                    break;
                case FactType.Float:
                    valueLabel.text = conditionData.FloatRequired.ToString();
                    break;
                case FactType.String:
                    valueLabel.text = conditionData.StringRequired;
                    break;
            }
        }

        public void UpdateMinFactLabelWidth(float minWidth)
        {
            factLabel.style.minWidth = minWidth;
        }
        
        private void OnLabelStyleResolved()
        {
            float labelWidth = factLabel.resolvedStyle.width;
            
            if(requirementsListView.minFactLabelWidth < labelWidth)
                requirementsListView.UpdateMinFactLabelWidth(labelWidth);
        }
        
        /*public override void EnableDebugMode(Condition condition)
        {
            fulfilledToggle.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            fulfilledToggle.value = condition.IsFulfilled;
            condition.onFulfilled += e => OnFulfilled();
            condition.onUnfulfilled += e => OnUnfulfilled();
        }

        public override void DisableDebugMode()
        {
            fulfilledToggle.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }*/
    }
}
using System;
using System.IO;
using Blackboard.Requirement;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class ConditionView : VisualElement
    {
        private readonly string uxmlName = "UXML/Condition.uxml";
    
        public Action<ConditionSO> onConditionTypeChanged;

        private VisualElement conditionRoot;
        private BlackboardElementDropdown blackboardElementDropdown;

        private ConditionSO condition;
        private ConditionHandler conditionHandler;
    
        public ConditionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            conditionRoot = this.Q<VisualElement>("requirement-root");
            blackboardElementDropdown = this.Q<BlackboardElementDropdown>("blackboard-element");
            blackboardElementDropdown.SetElementTypesAllowed(BlackboardElementType.Fact, BlackboardElementType.Event);
            blackboardElementDropdown.onElementSelected += SetElement;
        }

        public void BindCondition(ConditionSO condition)
        {
            if(this.condition == condition)
                return;
        
            this.condition = condition;
        
            conditionHandler = ConditionHandlerFactory.CreateConditionHandler(condition);
            conditionHandler.onValueTypeChanged += OnValueTypeChanged;
        
            var elementRequired = condition.GetElementRequired();
        
            if(elementRequired != null)
            {
                conditionHandler.SetElement(elementRequired);
                blackboardElementDropdown.SetElement(elementRequired);
                
                ConditionGoalView conditionGoal = ConditionGoalViewFactory.CreateRequirementGoalView(condition);
                conditionGoal.BindCondition(condition);
            
                if(elementRequired.BlackboardElementType == BlackboardElementType.Event)
                    conditionRoot.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);
            
                if (conditionRoot.childCount > 1)
                {
                    conditionRoot.RemoveAt(1);    
                }
            
                conditionRoot.Add(conditionGoal);    
            }
        }

        public void SetElement(BlackboardElementSO elementSelected)
        {
            switch (elementSelected.BlackboardElementType)
            {
                case BlackboardElementType.Fact:
                    if (condition == null || conditionHandler.type != ConditionType.Fact)
                    {
                        ChangeRequirementType(ConditionType.Fact);
                        conditionRoot.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
                    }
                    break;
            
                case BlackboardElementType.Event:
                    if (condition == null || conditionHandler.type != ConditionType.Event)
                    {
                        ChangeRequirementType(ConditionType.Event);
                        conditionRoot.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Column);
                    }
                    break;
            }
        
            conditionHandler.SetElement(elementSelected);
        }
    
        private void ChangeRequirementType(ConditionType conditionType)
        {
            condition = ConditionSOFactory.CreateCondition(conditionType);
        
            conditionHandler = ConditionHandlerFactory.CreateConditionHandler(condition);
            conditionHandler.onValueTypeChanged += OnValueTypeChanged;
        
            onConditionTypeChanged?.Invoke(condition);
        }

        private void OnValueTypeChanged()
        {
            if (conditionRoot.childCount > 1)
            {
                conditionRoot.RemoveAt(1);    
            }
        
            ConditionGoalView conditionGoal = ConditionGoalViewFactory.CreateRequirementGoalView(condition);
            conditionGoal.BindCondition(condition);
            conditionRoot.Add(conditionGoal);
        }
    }
}
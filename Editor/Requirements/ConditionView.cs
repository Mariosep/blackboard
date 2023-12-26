using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class ConditionView : VisualElement
{
    private readonly string uxmlName = "UXML/Condition.uxml";
    
    public Action<ConditionSO> onConditionTypeChanged;

    private VisualElement requirementRoot;
    private BlackboardElementDropdown blackboardElementDropdown;

    private ConditionSO condition;
    private ConditionHandler conditionHandler;
    
    public ConditionView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        requirementRoot = this.Q<VisualElement>("requirement-root");
        blackboardElementDropdown = this.Q<BlackboardElementDropdown>("blackboard-element");
        blackboardElementDropdown.SetElementTypesAllowed(BlackboardElementType.Fact, BlackboardElementType.Event);
        blackboardElementDropdown.onElementSelected += SetElement;
    }

    public void BindCondition(ConditionSO condition)
    {
        if(this.condition == condition || condition is EmptyConditionSO)
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
            
            if (requirementRoot.childCount > 1)
            {
                requirementRoot.RemoveAt(1);    
            }
            
            requirementRoot.Add(conditionGoal);    
        }
    }

    public void SetElement(BlackboardElementSO elementSelected)
    {
        switch (elementSelected.blackboardElementType)
        {
            case BlackboardElementType.Fact:
                if (condition == null || conditionHandler.type != ConditionType.Fact)
                    ChangeRequirementType(ConditionType.Fact);
                break;
            
            case BlackboardElementType.Event:
                if (condition == null || conditionHandler.type != ConditionType.Event)
                    ChangeRequirementType(ConditionType.Event);
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
        if (requirementRoot.childCount > 1)
        {
            requirementRoot.RemoveAt(1);    
        }
        
        ConditionGoalView conditionGoal = ConditionGoalViewFactory.CreateRequirementGoalView(condition);
        conditionGoal.BindCondition(condition);
        requirementRoot.Add(conditionGoal);
    }
}
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class ConditionGoalEventArgView : ConditionGoalView
{
    private readonly string uxmlName = "UXML/RequirementGoalEventArg.uxml";

    private BlackboardElementDropdown argumentRequiredDropdown;

    private EventConditionSO eventCondition;
    
    public ConditionGoalEventArgView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        argumentRequiredDropdown = this.Q<BlackboardElementDropdown>("requirement-argument");
        argumentRequiredDropdown.onElementSelected += SetArgumentRequired;
    }

    public override void BindCondition(ConditionSO condition)
    {
        eventCondition = (EventConditionSO) condition;

        if (eventCondition.EventType == EventType.Item)
        {
            argumentRequiredDropdown.SetElementTypesAllowed(BlackboardElementType.Item);
            
            if(eventCondition.itemArgRequired != null)
                argumentRequiredDropdown.SetElement(eventCondition.itemArgRequired);
        }
        else if (eventCondition.EventType == EventType.Actor)
        {
            argumentRequiredDropdown.SetElementTypesAllowed(BlackboardElementType.Actor);
            
            if(eventCondition.actorArgRequired != null)
                argumentRequiredDropdown.SetElement(eventCondition.actorArgRequired);
        }
    }
    
    private void SetArgumentRequired(BlackboardElementSO elementSelected)
    {
        eventCondition.SetArgRequired(elementSelected);
    }
}
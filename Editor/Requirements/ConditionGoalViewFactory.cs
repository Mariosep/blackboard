using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public static class ConditionGoalViewFactory
{
    public static ConditionGoalView CreateRequirementGoalView(ConditionSO condition)
    {
        switch (condition.type)
        {
            case ConditionType.Fact:
                return new ConditionGoalFactValueView();
            
            case ConditionType.Event:
                return new ConditionGoalEventArgView();
        }

        return null;
    }

    public static VisualElement CreateGoalFactValueField(FactConditionSO factCondition)
    {
        var serializedObject = new SerializedObject(factCondition);
        
        return factCondition.fact.type switch
        {
            FactType.Bool => CreateToggle(serializedObject),
            FactType.Int => CreateIntegerField(serializedObject),
            FactType.Float => CreateFloatField(serializedObject),
            FactType.String => CreateTextField(serializedObject),
            _ => new VisualElement()
        };
    }
    
    public static Toggle CreateToggle(SerializedObject serializedObject)
    {
        var boolRequiredProperty = serializedObject.FindProperty("boolRequired");
        
        var toggle = new Toggle("");
        toggle.BindProperty(boolRequiredProperty);
        
        return toggle;
    }
    
    public static IntegerField CreateIntegerField(SerializedObject serializedObject)
    {
        var intRequiredProperty = serializedObject.FindProperty("intRequired");
        
        var integerField = new IntegerField("");
        integerField.BindProperty(intRequiredProperty);
        
        return integerField;
    }
    
    public static FloatField CreateFloatField(SerializedObject serializedObject)
    {
        var floatRequiredProperty = serializedObject.FindProperty("floatRequired");
        
        var floatField = new FloatField("");
        floatField.BindProperty(floatRequiredProperty);
        
        return floatField;
    }
    
    public static TextField CreateTextField(SerializedObject serializedObject)
    {
        var stringRequiredProperty = serializedObject.FindProperty("stringRequired");
        
        var textField = new TextField("");
        textField.BindProperty(stringRequiredProperty);
        textField.style.minWidth = 80;
        
        return textField;
    }
}
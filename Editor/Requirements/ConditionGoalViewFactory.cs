using Blackboard.Facts;
using Blackboard.Requirement;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public static class ConditionGoalViewFactory
    {
        public static ConditionGoalView CreateRequirementGoalView(ConditionSO condition)
        {
            switch (condition.type)
            {
                case ConditionType.Fact:
                    return new ConditionGoalFactValueView();
            
                case ConditionType.Event:
                    return new ConditionGoalEventView();
            }

            return null;
        }

        public static VisualElement CreateGoalFactValueField(FactConditionSO factCondition)
        {
            var serializedObject = new SerializedObject(factCondition);
        
            return factCondition.fact.type switch
            {
                FactType.Bool => CreateToggle(serializedObject, factCondition),
                FactType.Int => CreateIntegerField(serializedObject, factCondition),
                FactType.Float => CreateFloatField(serializedObject, factCondition),
                FactType.String => CreateTextField(serializedObject, factCondition),
                _ => new VisualElement()
            };
        }
    
        public static Toggle CreateToggle(SerializedObject serializedObject, FactConditionSO factCondition)
        {
            var boolRequiredProperty = serializedObject.FindProperty("boolRequired");
        
            var toggle = new Toggle("");
            toggle.BindProperty(boolRequiredProperty);
            toggle.RegisterValueChangedCallback(e =>
            {
                factCondition.BoolRequired = e.newValue;
            });
        
            return toggle;
        }
    
        public static IntegerField CreateIntegerField(SerializedObject serializedObject, FactConditionSO factCondition)
        {
            var intRequiredProperty = serializedObject.FindProperty("intRequired");
        
            var integerField = new IntegerField("");
            integerField.isDelayed = true;
            integerField.BindProperty(intRequiredProperty);
            integerField.RegisterValueChangedCallback(e =>
            {
                factCondition.IntRequired = e.newValue;
            });
        
            return integerField;
        }
    
        public static FloatField CreateFloatField(SerializedObject serializedObject, FactConditionSO factCondition)
        {
            var floatRequiredProperty = serializedObject.FindProperty("floatRequired");
        
            var floatField = new FloatField("");
            floatField.isDelayed = true;
            floatField.BindProperty(floatRequiredProperty);
            floatField.RegisterValueChangedCallback(e =>
            {
                factCondition.FloatRequired = e.newValue;
            });
        
            return floatField;
        }
    
        public static TextField CreateTextField(SerializedObject serializedObject, FactConditionSO factCondition)
        {
            var stringRequiredProperty = serializedObject.FindProperty("stringRequired");
        
            var textField = new TextField("");
            textField.isDelayed = true;
            textField.BindProperty(stringRequiredProperty);
            textField.style.minWidth = 80;
            textField.RegisterValueChangedCallback(e =>
            {
                factCondition.StringRequired = e.newValue;
            });
        
            return textField;
        }
    }
}
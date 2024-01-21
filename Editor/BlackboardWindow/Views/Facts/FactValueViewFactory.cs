using Blackboard.Facts;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public static class FactValueViewFactory
    {
        public static VisualElement CreateValueView(FactSO fact)
        {
            return fact switch
            {
                BoolFactSO bFact => CreateToggle(bFact),
                IntFactSO iFact => CreateIntegerField(iFact),
                FloatFactSO fFact => CreateFloatField(fFact),
                StringFactSO sFact => CreateTextField(sFact),
                _ => new VisualElement()
            };
        }

        private static VisualElement CreateTextField(StringFactSO fact)
        {
            var textFieldItem = new TextFieldItem("_value");
            textFieldItem.SetDataSource(fact);
            textFieldItem.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetValue(e.newValue, fact));
            return textFieldItem;
        }

        private static VisualElement CreateIntegerField(IntFactSO fact)
        {
            var integerFieldItem = new IntegerFieldItem("_value");
            integerFieldItem.SetDataSource(fact);
            integerFieldItem.integerField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetValue(e.newValue, fact));
        
            return integerFieldItem;
        }

        private static VisualElement CreateFloatField(FloatFactSO fact)
        {
            var floatFieldItem = new FloatFieldItem("_value");
            floatFieldItem.SetDataSource(fact);
            floatFieldItem.floatField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetValue(e.newValue, fact));
        
            return floatFieldItem;
        }

        private static VisualElement CreateToggle(BoolFactSO fact)
        {
            var toggleItem = new ToggleItem("_value");
            toggleItem.SetDataSource(fact);
            toggleItem.toggle.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetValue(e.newValue, fact));
        
            return toggleItem;
        }
    }
}
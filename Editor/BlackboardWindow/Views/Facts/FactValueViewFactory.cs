using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

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
    
    public static TextField CreateTextField(StringFactSO fact)
    {
        var serializedObject = new SerializedObject(fact);
        var valueProperty = serializedObject.FindProperty("_value");
        
        var textField = new TextField("");
        textField.value = fact.Value;
        textField.BindProperty(valueProperty);

        textField.RegisterCallback<FocusOutEvent>(_ =>
        {
            textField.value = textField.value.Trim();
            fact.Value = textField.value;
        });
        
        return textField;
    }
    
    public static IntegerField CreateIntegerField(IntFactSO fact)
    {
        var serializedObject = new SerializedObject(fact);
        var valueProperty = serializedObject.FindProperty("_value");
        
        var integerField = new IntegerField("");
        integerField.value = fact.Value;
        integerField.BindProperty(valueProperty);

        integerField.RegisterValueChangedCallback(e =>
        {
            if(e.previousValue == e.newValue)
                return;
            
            fact.Value = e.newValue;
        });
        
        return integerField;
    }
    
    public static FloatField CreateFloatField(FloatFactSO fact)
    {
        var serializedObject = new SerializedObject(fact);
        var valueProperty = serializedObject.FindProperty("_value");
        
        var floatField = new FloatField("");
        floatField.value = fact.Value;
        floatField.BindProperty(valueProperty);

        floatField.RegisterValueChangedCallback(e =>
        {
            if(e.previousValue == e.newValue)
                return;
            
            fact.Value = e.newValue;
        });
        
        return floatField;
    }
    
    public static Toggle CreateToggle(BoolFactSO fact)
    {
        var serializedObject = new SerializedObject(fact);
        var valueProperty = serializedObject.FindProperty("_value");
        
        var toggle = new Toggle("");
        toggle.value = fact.Value;
        toggle.BindProperty(valueProperty);

        toggle.RegisterValueChangedCallback(e =>
        {
            if(e.previousValue == e.newValue)
                return;
            
            fact.Value = e.newValue;
        });
        
        return toggle;
    }
}
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class FloatFactView : FactView
{
    private FloatFactSO floatFact;
    
    private FloatField floatField;
    
    protected override string ValueViewUxmlPath => "UXML/FloatFactValue.uxml";
    
    public FloatFactView(bool showValue = false) : base(showValue)
    {
        factDropdown.SetFactType(FactType.Float);
    }
    public override void SetFact(FactSO factSelected)
    {
        if (factSelected is FloatFactSO fFactSelected)
        {
            floatFact = fFactSelected;
            
            factDropdown.SetFact(factSelected);
            
            if(showValue)
                BindValue();
            
            onFactSelected?.Invoke(factSelected);
        }
    }
    
    private void BindValue()
    {
        if(!valueContainer.visible)
            ShowValue();

        var so = new SerializedObject(floatFact);
        floatField.BindProperty(so.FindProperty("_value"));
    }
    
    protected override void AddValueField()
    {
        base.AddValueField();
        
        floatField = factDropdown.Q<FloatField>();
    }
}
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(FloatFact))]
public class FloatFactDrawer : FactDrawer
{
    private const string UxmlPath = "UXML/FloatFactValue.uxml";
    
    private SerializedProperty ffactProperty;
    private SerializedProperty fFactSOProperty;
    
    private VisualElement valueContent;
    private FloatField floatField;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        base.CreatePropertyGUI(property);

        ffactProperty = property;
        fFactSOProperty = property.FindPropertyRelative("fact");

        AddValueField();
        
        if (fFactSOProperty.objectReferenceValue != null)
            BindValue();
        
        return factDropdown;
    }

    private void AddValueField()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(factDropdown);
        
        valueContent = factDropdown.Q<VisualElement>("value-content");
        valueContent.SetEnabled(false);
        valueContent.visible = false;

        valueContent.style.marginTop = 5;
        
        floatField = factDropdown.Q<FloatField>();
    }
    
    private void ShowValue()
    {
        valueContent.visible = true;
    }
    
    private void BindValue()
    {
        if(!valueContent.visible)
            ShowValue();

        var so = new SerializedObject(fFactSOProperty.objectReferenceValue);
        floatField.BindProperty(so.FindProperty("_value"));
    }
    
    protected override void SetFactValue(FactSO factSelected)
    {
        base.SetFactValue(factSelected);

        if (factSelected is FloatFactSO fFactSelected)
        {
            FloatFact ffact = (FloatFact)SerializedPropertyUtility.GetPropertyInstance(ffactProperty);
            
            fFactSOProperty.objectReferenceValue = fFactSelected;
            ffact.SetFact(fFactSelected);
            
            BindValue();
            
            ffactProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
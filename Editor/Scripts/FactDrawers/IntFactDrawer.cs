using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(IntFact))]
public class IntFactDrawer : FactDrawer
{
    private const string UxmlPath = "UXML/IntFactValue.uxml";
    
    private SerializedProperty ifactProperty;
    private SerializedProperty iFactSOProperty;
    
    private VisualElement valueContent;
    private IntegerField intField;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        base.CreatePropertyGUI(property);

        ifactProperty = property;
        iFactSOProperty = property.FindPropertyRelative("fact");

        AddValueField();
        
        if (iFactSOProperty.objectReferenceValue != null)
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

        intField = factDropdown.Q<IntegerField>();
    }
    
    private void ShowValue()
    {
        valueContent.visible = true;
    }
    
    private void BindValue()
    {
        if(!valueContent.visible)
            ShowValue();

        var so = new SerializedObject(iFactSOProperty.objectReferenceValue);
        intField.BindProperty(so.FindProperty("_value"));
    }

    
    protected override void SetFactValue(FactSO factSelected)
    {
        base.SetFactValue(factSelected);

        if (factSelected is IntFactSO iFactSelected)
        {
            IntFact ifact = (IntFact)SerializedPropertyUtility.GetPropertyInstance(ifactProperty);
            
            iFactSOProperty.objectReferenceValue = iFactSelected;
            ifact.SetFact(iFactSelected);
            
            BindValue();
            
            ifactProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
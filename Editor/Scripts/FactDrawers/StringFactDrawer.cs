using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(StringFact))]
public class StringFactDrawer : FactDrawer
{
    private const string UxmlPath = "UXML/StringFactValue.uxml";
    
    private SerializedProperty sfactProperty;
    private SerializedProperty sFactSOProperty;

    private VisualElement valueContent;
    private TextField stringField;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        base.CreatePropertyGUI(property);

        sfactProperty = property;
        sFactSOProperty = property.FindPropertyRelative("fact");

        AddValueField();
        
        if (sFactSOProperty.objectReferenceValue != null)
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
        
        stringField = factDropdown.Q<TextField>();
    }
    
    private void ShowValue()
    {
        valueContent.visible = true;
    }
    
    private void BindValue()
    {
        if(!valueContent.visible)
            ShowValue();

        var so = new SerializedObject(sFactSOProperty.objectReferenceValue);
        stringField.BindProperty(so.FindProperty("_value"));
    }
    
    protected override void SetFactValue(FactSO factSelected)
    {
        base.SetFactValue(factSelected);

        if (factSelected is StringFactSO sFactSelected)
        {
            StringFact sfact = (StringFact)SerializedPropertyUtility.GetPropertyInstance(sfactProperty);
            
            sFactSOProperty.objectReferenceValue = sFactSelected;
            sfact.SetFact(sFactSelected);
            
            BindValue();

            sfactProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
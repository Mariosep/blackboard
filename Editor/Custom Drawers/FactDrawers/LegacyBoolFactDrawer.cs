/*using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(BoolFact))]
public class LegacyBoolFactDrawer : FactDrawer
{
    private const string UxmlPath = "UXML/BoolFactValue.uxml";
    
    private SerializedProperty bfactProperty;
    private SerializedProperty bFactSOProperty;

    private VisualElement valueContent;
    private Toggle toggle;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        base.CreatePropertyGUI(property);

        bfactProperty = property;
        bFactSOProperty = property.FindPropertyRelative("fact");

        AddValueField();
        
        if (bFactSOProperty.objectReferenceValue != null)
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
        
        toggle = factDropdown.Q<Toggle>();
    }
    
    private void ShowValue()
    {
        valueContent.visible = true;
    }

    private void BindValue()
    {
        if(!valueContent.visible)
            ShowValue();

        var so = new SerializedObject(bFactSOProperty.objectReferenceValue);
        toggle.BindProperty(so.FindProperty("_value"));
    }
    
    protected override void SetFactValue(FactSO factSelected)
    {
        base.SetFactValue(factSelected);

        if (factSelected is BoolFactSO bFactSelected)
        {
            BoolFact bfact = (BoolFact)SerializedPropertyUtility.GetPropertyInstance(bfactProperty);

            bFactSOProperty.objectReferenceValue = bFactSelected;
            bfact.SetFact(bFactSelected);

            BindValue();
            
            bfactProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}*/
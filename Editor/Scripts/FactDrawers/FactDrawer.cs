using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Fact<FactSO,object>), true)]
public class FactDrawer : PropertyDrawer
{
    private SerializedProperty factProperty;

    protected SerializedProperty idProperty;
    protected SerializedProperty categoryIdProperty;
    protected SerializedProperty factTypeProperty;
    private SerializedProperty factSOProperty;

    protected FactDropdown factDropdown;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Get properties
        factProperty = property;
        idProperty = property.FindPropertyRelative("id");
        categoryIdProperty = property.FindPropertyRelative("categoryId");
        factTypeProperty = property.FindPropertyRelative("factType");
        factSOProperty = property.FindPropertyRelative("fact");
        
        var factSo = (FactSO) factSOProperty.objectReferenceValue;
        
        factDropdown = new FactDropdown();
        factDropdown.SetName(property.name.Capitalize());
        
        int enumIndex = factTypeProperty.intValue;
        FactType factType = (FactType)enumIndex;
        
        factDropdown.SetFactType(factType);
        
        if(factSo != null)
            factDropdown.SetFact(factSo);

        factDropdown.onFactSelected += SetFact;

        factDropdown.style.marginTop = 5;
        factDropdown.style.marginBottom = 10;
        
        return factDropdown;        
    }
    
    protected FactSO GetFact(string categoryId, string factId)
    {
        return BlackboardManager.instance.GetFact(categoryId, factId);
    }
    
    private void SetFact(FactSO factSelected)
    {
        idProperty.stringValue = factSelected.id;
        categoryIdProperty.stringValue = factSelected.groupId;

        SetFactValue(factSelected);
        
        factProperty.serializedObject.ApplyModifiedProperties();
    }

    protected virtual void SetFactValue(FactSO factSelected) {}
}
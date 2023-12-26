using System;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(FactSO), true)]
public class FactDrawer : PropertyDrawer
{
    protected SerializedProperty factProperty;
    private FactSO factSelected;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        factProperty = property;
        factSelected = (FactSO)property.objectReferenceValue;
        
        Type factType = GetFieldType(property);
        FactView factView = FactViewFactory.CreateFactView(factType);
        factView.SetName(property.name);
        
        factView.onFactSelected += SetFact;
        
        if (factSelected != null)
            factView.SetFact(factSelected);

        factView.onFactSelected += SetFact;
        
        return factView;
    }
    
    private void SetFact(FactSO newFactSelected)
    {
        factSelected = newFactSelected;
        factProperty.objectReferenceValue = newFactSelected;
        factProperty.serializedObject.ApplyModifiedProperties();
    }
    
    private Type GetFieldType(SerializedProperty property)
    {
        // Assuming it's an object reference property
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            // Get the serialized object
            var serializedObject = property.serializedObject.targetObject;

            // Use reflection to get the type of the property
            System.Type objectType = serializedObject.GetType();
            System.Reflection.FieldInfo fieldInfo = objectType.GetField(property.name);
            System.Type fieldType = fieldInfo.FieldType;

            return fieldType;
        }

        return null; // Property type is not an object reference
    }
}
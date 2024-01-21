using System;
using System.Reflection;
using Blackboard.Editor.Facts;
using Blackboard.Facts;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    [CustomPropertyDrawer(typeof(ShowValueAttribute))]
    public class ShowValueAttributeDrawer : PropertyDrawer
    {
        private SerializedProperty factProperty;
        private FactSO factSelected;
    
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            factProperty = property;
            factSelected = (FactSO)property.objectReferenceValue;
        
            var showValueAttribute = attribute as ShowValueAttribute;
            if(showValueAttribute != null)
            {
                Type factType = GetFieldType(property);

                FactView factView = FactViewFactory.CreateFactView(factType, true);
                factView.SetName(property.name);

                if (factSelected != null)
                    factView.SetFact(factSelected);

                factView.onFactSelected += SetFact;

                return factView;
            }
        
            return new Label("Error Label");    
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
                Type objectType = serializedObject.GetType();
                FieldInfo fieldInfo = objectType.GetField(property.name);
                Type fieldType = fieldInfo.FieldType;

                return fieldType;
            }

            return null; // Property type is not an object reference
        }
    }
}
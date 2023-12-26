using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(RequirementsSO))]
public class RequirementDrawer : PropertyDrawer
{
    private SerializedProperty requirementProperty;
    private RequirementsSO requirements;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        requirementProperty = property;
        requirements = (RequirementsSO)property.objectReferenceValue;

        if(requirements == null)
        {
            requirements = ScriptableObject.CreateInstance<RequirementsSO>();
            requirementProperty.objectReferenceValue = requirements;
            requirementProperty.serializedObject.ApplyModifiedProperties();
        }
        
        RequirementsListView requirementsListView = new RequirementsListView();
        requirementsListView.Populate(requirements);

        return requirementsListView;
    }
 }

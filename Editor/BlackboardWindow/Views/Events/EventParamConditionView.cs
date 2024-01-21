using System.IO;
using Blackboard.Requirement;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class EventParamConditionView : VisualElement
    {
        private readonly string uxmlName = "UXML/EventParamCondition.uxml";
        
        // Visual elements
        private VisualElement paramContent;
        private Toggle isRequiredToggle;
        private PropertyField paramValueRequiredField;

        public EventParamConditionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            paramContent = this.Q<VisualElement>("param-content");
            isRequiredToggle = this.Q<Toggle>();
        }

        public void BindArgument(EventConditionSO eventCondition, SerializedProperty paramIsRequiredProperty, SerializedProperty paramProperty)
        {
            isRequiredToggle.RegisterValueChangedCallback(e => paramValueRequiredField.SetEnabled(e.newValue));
            isRequiredToggle.BindProperty(paramIsRequiredProperty);
            isRequiredToggle.RegisterValueChangedCallback(e =>
            {
                eventCondition.onConditionModified?.Invoke();
            });

            paramValueRequiredField = new PropertyField(paramProperty);
            paramValueRequiredField.bindingPath = paramProperty.name;
            paramValueRequiredField.style.minWidth = 180;
            paramValueRequiredField.RegisterValueChangeCallback(e =>
            {
                eventCondition.onConditionModified?.Invoke();
            });
            
            paramContent.Add(paramValueRequiredField);
        }
    }
}
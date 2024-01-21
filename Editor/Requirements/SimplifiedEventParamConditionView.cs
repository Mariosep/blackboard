using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class SimplifiedEventParamConditionView : VisualElement
    {
        private readonly string uxmlName = "UXML/SimplifiedEventParamCondition.uxml";

        private VisualElement paramContent;
        private Label paramName;
        private Label paramValue;
        
        public SimplifiedEventParamConditionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            paramContent = this.Q<VisualElement>("param-content");
            paramName = this.Q<Label>("param-name__label");
            paramValue = this.Q<Label>("param-value__label");
        }

        public void BindParam(SerializedProperty paramProperty)
        {
            paramName.text = paramProperty.displayName;
            
            paramValue.bindingPath = paramProperty.name;
            paramValue.BindProperty(paramProperty);
        }

        /*public void UpdateMinEventLabelWidth(float minWidth)
        {
            paramName.style.minWidth = minWidth - paramContent.resolvedStyle.marginLeft;
        }*/
    }
}
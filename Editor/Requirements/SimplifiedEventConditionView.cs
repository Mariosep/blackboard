using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Blackboard.Requirement;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class SimplifiedEventConditionView : SimplifiedConditionView
    {
        private readonly string uxmlName = "UXML/SimplifiedEventCondition.uxml";
        
        private EventConditionSO condition;

        private List<SimplifiedEventParamConditionView> paramViewList;
        
        private Label eventLabel;
        private VisualElement paramsContent;
        private Label valueLabel;
        
        public SimplifiedEventConditionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            fulfilledToggle = this.Q<Toggle>();
            eventLabel = this.Q<Label>("event__label");
            paramsContent = this.Q<VisualElement>("params-content");
        }
        
        public override void BindCondition(SimplifiedRequirementsListView requirementsListView, ConditionSO condition)
        {
            this.requirementsListView = requirementsListView;
            this.condition = (EventConditionSO)condition;

            condition.onConditionModified += UpdateContent;
            
            //this.requirementsListView.onMinFactLabelWidthModified += UpdateMinEventLabelWidth;
            
            UpdateContent();
        }

        protected override void UpdateContent()
        {
            eventLabel.text = condition.eventRequired.Name;
            
            paramViewList = new List<SimplifiedEventParamConditionView>();
            paramsContent.Clear();
            
            SerializedObject serializedEvent = new SerializedObject(condition.eventRequired);
            SerializedObject serializedCondition = new SerializedObject(condition);
            SerializedProperty serializedParamValuesRequired = serializedCondition.FindProperty("paramValuesRequired");
        
            var iterator = serializedEvent.GetIterator();
            iterator.NextVisible(true);

            int paramCount = 0;
            while (iterator.NextVisible(false))
            {
                var field = condition.eventRequired.GetType()
                    .GetField(iterator.name, BindingFlags.Instance | BindingFlags.Public);
                if (field != null)
                {
                    // Check if the field has the ParameterAttribute
                    if (Attribute.IsDefined(field, typeof(ParameterAttribute)))
                    {
                        SerializedProperty paramValueRequired = serializedParamValuesRequired.GetArrayElementAtIndex(paramCount);
                        if (paramValueRequired.boolValue)
                        {
                            var paramCondition = new SimplifiedEventParamConditionView();
                            paramCondition.BindParam(iterator);
                            paramsContent.Add(paramCondition);

                            paramViewList.Add(paramCondition);
                        }

                        paramCount++;
                    }
                }
            }

            this.Bind(serializedEvent);
        }

        /*public override void EnableDebugMode(Condition condition)
        {
            
        }

        public override void DisableDebugMode()
        {
            
        }*/

        /*private void UpdateMinEventLabelWidth(float minWidth)
        {
            foreach (var paramConditionView in paramViewList)
                paramConditionView.UpdateMinEventLabelWidth(minWidth);    
        }*/
    }
}
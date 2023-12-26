using System;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Actions
{
    public class ActionView : VisualElement
    {
        private readonly string uxmlName = "UXML/Action.uxml";

        public Action<Action> onActionSelected;
        
        private ActionDropdown actionDropdown;
        private VisualElement actionContent;
        
        private Action action;

        public ActionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            actionDropdown = this.Q<ActionDropdown>();
            actionContent = this.Q<VisualElement>("action-content");
            
            actionDropdown.onActionSelected += SetAction;
        }
        
        public void SetAction(Action actionSelected)
        {
            if(this.action == actionSelected)
                return;

            action = actionSelected;
            
            actionDropdown.SetAction(actionSelected);
            UpdateActionContent();
            
            onActionSelected?.Invoke(actionSelected);
        }
        
        private void UpdateActionContent()
        {
            actionDropdown.UpdateButtonText();
            
            actionContent.Clear();
            
            var serializedAction = new SerializedObject(action);
            
            var iterator = serializedAction.GetIterator();
            iterator.NextVisible(true);
            
            while (iterator.NextVisible(false))
            {
                var propertyField = new PropertyField(iterator);
                propertyField.bindingPath = iterator.name;
                actionContent.Add(propertyField);
            }
            
            actionContent.Bind(serializedAction);
        }
    }
}
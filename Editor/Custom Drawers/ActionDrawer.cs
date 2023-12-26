using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Actions
{
    [CustomPropertyDrawer(typeof(Action))]
    public class ActionDrawer : PropertyDrawer
    {
        private SerializedProperty actionProperty;

        private Action actionSelected;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            actionProperty = property;
            actionSelected = (Action) property.objectReferenceValue;

            var actionView = new ActionView();
            
            if(actionSelected != null)
                actionView.SetAction(actionSelected);

            actionView.onActionSelected += SetAction;
            
            return actionView;
        }

        private void SetAction(Action newActionSelected)
        {
            actionSelected = newActionSelected;
            actionProperty.objectReferenceValue = newActionSelected;
            actionProperty.serializedObject.ApplyModifiedProperties();
            
            Debug.Log("Action selected");
        }
    }    
}


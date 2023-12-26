using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Actions
{
    [CustomPropertyDrawer(typeof(ActionList))]
    public class ActionsListDrawer : PropertyDrawer
    {
        private SerializedProperty actionListProperty;
        private ActionList actionList;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            actionListProperty = property;
            actionList = (ActionList)property.objectReferenceValue;
            
            if(actionList == null)
            {
                actionList = ScriptableObject.CreateInstance<ActionList>();
                actionListProperty.objectReferenceValue = actionList;
                actionListProperty.serializedObject.ApplyModifiedProperties();
            }
            
            ActionListView actionListView = new ActionListView();
            actionListView.Populate(actionList);

            string headerName = actionListProperty.name.Capitalize();
            headerName = Regex.Replace(headerName, "([a-z])([A-Z])", "$1 $2");
            
            actionListView.SetHeaderTitle(headerName);
            
            return actionListView;
        }
    }    
}

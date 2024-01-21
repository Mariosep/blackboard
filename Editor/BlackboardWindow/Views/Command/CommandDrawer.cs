using Blackboard.Commands;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Commands
{
    [CustomPropertyDrawer(typeof(Command))]
    public class CommandDrawer : PropertyDrawer
    {
        private SerializedProperty commandProperty;

        private Command commandSelected;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            commandProperty = property;
            commandSelected = (Command) property.objectReferenceValue;

            var commandView = new CommandView();
            
            if(commandSelected != null)
                commandView.SetCommand(commandSelected);

            commandView.onCommandSelected += SetCommand;
            
            return commandView;
        }

        private void SetCommand(Command newCommandSelected)
        {
            commandSelected = newCommandSelected;
            commandProperty.objectReferenceValue = newCommandSelected;
            commandProperty.serializedObject.ApplyModifiedProperties();
            
            Debug.Log("Command selected");
        }
    }    
}


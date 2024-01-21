using System.Text.RegularExpressions;
using Blackboard.Commands;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Commands
{
    [CustomPropertyDrawer(typeof(CommandList))]
    public class CommandListDrawer : PropertyDrawer
    {
        private SerializedProperty commandListProperty;
        private CommandList commandList;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            commandListProperty = property;
            commandList = (CommandList)property.objectReferenceValue;
            
            if(commandList == null)
            {
                commandList = ScriptableObject.CreateInstance<CommandList>();
                commandListProperty.objectReferenceValue = commandList;
                commandListProperty.serializedObject.ApplyModifiedProperties();
            }
            
            CommandListView commandListView = new CommandListView();
            commandListView.PopulateView(commandList);

            string headerName = commandListProperty.name.Capitalize();
            headerName = Regex.Replace(headerName, "([a-z])([A-Z])", "$1 $2");
            
            commandListView.SetHeaderTitle(headerName);
            
            return commandListView;
        }
    }    
}

using System;
using System.IO;
using Blackboard.Commands;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Commands
{
    public class CommandView : VisualElement
    {
        private readonly string uxmlName = "UXML/Command.uxml";

        public Action<Command> onCommandSelected;
        
        private CommandDropdown commandDropdown;
        private VisualElement commandContent;
        
        private Command command;

        public CommandView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            commandDropdown = this.Q<CommandDropdown>();
            commandContent = this.Q<VisualElement>("command-content");
            
            commandDropdown.onCommandSelected += SetCommand;
        }
        
        public void SetCommand(Command commandSelected)
        {
            if(this.command == commandSelected)
                return;

            command = commandSelected;
            
            commandDropdown.SetCommand(commandSelected);
            UpdateCommandContent();
            
            onCommandSelected?.Invoke(commandSelected);
        }
        
        private void UpdateCommandContent()
        {
            commandDropdown.UpdateButtonText();
            
            commandContent.Clear();
            
            var serializedCommand = new SerializedObject(command);
            
            var iterator = serializedCommand.GetIterator();
            iterator.NextVisible(true);
            
            while (iterator.NextVisible(false))
            {
                var propertyField = new PropertyField(iterator);
                propertyField.bindingPath = iterator.name;
                commandContent.Add(propertyField);
            }
            
            commandContent.Bind(serializedCommand);
        }
    }
}
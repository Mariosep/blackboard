using System;
using System.IO;
using Blackboard.Commands;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Commands
{
    public class CommandDropdown : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CommandDropdown, UxmlTraits>{}
        
        private const string UxmlPath = "UXML/ElementDropdown.uxml";
        
        public Action<Command> onCommandSelected;

        public Command commandSelected;

        private Button buttonPopup;

        public CommandDropdown()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            buttonPopup = this.Q<Button>();
            buttonPopup.clickable = new Clickable(() =>
            {
                CommandSearchWindow.Open(OnCommandSelected);
            });
        }

        private void OnCommandSelected(Type commandTypeSelected)
        {
            if (commandSelected != null && commandSelected.GetType() == commandTypeSelected)
                return;

            var newCommandSelected = ScriptableObject.CreateInstance(commandTypeSelected) as Command;

            SetCommand(newCommandSelected);
        }

        public void SetCommand(Command newCommandSelected)
        {
            commandSelected = newCommandSelected;

            UpdateButtonText();

            onCommandSelected?.Invoke(commandSelected);
        }

        public void UpdateButtonText()
        {
            if(commandSelected != null)
                buttonPopup.text = commandSelected.GetName();
        }
    }
}
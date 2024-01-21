using System;
using System.Collections.Generic;
using System.IO;
using Blackboard.Commands;
using Blackboard.Utils.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Commands
{
    // TODO: Refactor to extract duplicated code between this class and RequirementsListView
    public class CommandListView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CommandListView, CommandListUxmlTraits>{}
        
        private readonly string uxmlName = "UXML/CommandListView.uxml";
        
        // Visual elements
        private Foldout headerFoldout;
        protected ListView listView;

        private CommandList commandList;

        // State
        private bool foldoutExpanded;

        private List<Command> commands => commandList.commands;
        
        // Configuration
        private bool saveEnabled = false;
        private ScriptableObject mainAsset;
        
        // Properties
        public string headerTitle { get; set; }
        public bool collapdseByDefault { get; set; }
        
        public CommandListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            headerFoldout = this.Q<Foldout>("command-list-header__foldout");
            listView = this.Q<ListView>();
            
            Setup();
            RegisterCallbacks();
        }
        
        private void Setup()
        {
            foldoutExpanded = true;

            var toggle = headerFoldout.Q<Toggle>();
            toggle.style.marginLeft = new StyleLength(3);
            
            listView.makeItem = MakeItem;
            listView.bindItem = (element, i) => BindItem(element, i);
        }
 
        public void PopulateView(CommandList commandList)
        {
            this.commandList = commandList;
            listView.itemsSource = commands;
            listView.bindingPath = "commands";
        }

        public void SaveAsSubAssetOf(ScriptableObject mainAsset)
        {
            saveEnabled = true;
            this.mainAsset = mainAsset;
        }
        
        public void SetHeaderTitle(string newTitle)
        {
            headerTitle = newTitle;
            headerFoldout.text = newTitle;
        }
            
        private void RegisterCallbacks()
        {
            RegisterCallback<AttachToPanelEvent>(e =>
            {
                SetHeaderTitle(headerTitle);
                if(collapdseByDefault)
                    ToggleFoldout();
            });

            headerFoldout.RegisterValueChangedCallback(evt =>
            {
                if (evt.target == headerFoldout)
                {
                    SetFoldoutState(headerFoldout.value);
                }
            });
            
            listView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(() =>
            {
                CommandSearchWindow.Open(AddCommand);
            });
            
            listView.Q<Button>("unity-list-view__remove-button").clickable = new Clickable(() =>
            {
                if(listView.selectedIndex != -1)
                    RemoveCommand(commandList.commands[listView.selectedIndex]);
            });
        }

        private void ToggleFoldout()
        {
            foldoutExpanded = !foldoutExpanded;
            headerFoldout.value = foldoutExpanded;
        }
        
        public void SetFoldoutState(bool isExpanded)
        {
            foldoutExpanded = isExpanded;
        
            if (foldoutExpanded)
            {
                listView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }
            else
            {
                listView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }
        }
        
        protected virtual void AddCommand(Type actionType)
        {
            var newAction = ScriptableObject.CreateInstance(actionType) as Command;
            
            if(saveEnabled)
            {
                AssetDatabase.AddObjectToAsset(newAction, mainAsset);
                AssetDatabase.SaveAssets();
            }
            
            commands.Add(newAction);
            listView.RefreshItems();
        }

        protected virtual void RemoveCommand(Command command)
        {
            commands.Remove(command);

            if (saveEnabled)
            {
                AssetDatabase.RemoveObjectFromAsset(command);
                AssetDatabase.SaveAssets();
            }
            
            listView.RefreshItems();
        }
        
        private VisualElement MakeItem() => new CommandView();

        private void BindItem(VisualElement element, int i)
        {
            var commandView = element as CommandView;
    
            commandView.SetCommand(commands[i]);
            commandView.onCommandSelected = (newAction => OnActionTypeChanged(newAction, i));
        }
        
        protected virtual void OnActionTypeChanged(Command newCommand, int i)
        {
            if (saveEnabled)
            {
                ScriptableObjectUtility.DeleteSubAsset(commands[i]);
                ScriptableObjectUtility.SaveSubAsset(newCommand, mainAsset);
            }
            
            commands[i] = newCommand;
        
            listView.RefreshItems();       
        }
    }
    
    public class CommandListUxmlTraits : VisualElement.UxmlTraits
    {
        private UxmlStringAttributeDescription headerTitle = new() { name = "header-title", defaultValue = "Commands"};
        private UxmlBoolAttributeDescription collapsedByDefault = new() { name = "collapsed-by-default", defaultValue = false};
        
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var element = ve as CommandListView;

            element.headerTitle = headerTitle.GetValueFromBag(bag, cc);
            element.collapdseByDefault = collapsedByDefault.GetValueFromBag(bag, cc);
        }
    }
}
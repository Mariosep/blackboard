using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Actions
{
    public class ActionListView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ActionListView, ActionListUxmlTraits>{}

        private readonly string uxmlName = "UXML/ActionListView.uxml";
        
        // Visual elements
        private Foldout headerFoldout;
        protected ListView listView;

        private ActionList actionList;

        // State
        private bool foldoutExpanded;
        
        protected List<Action> actions => actionList.actions;
        
        // Properties
        public string headerTitle { get; set; }

        public ActionListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            headerFoldout = this.Q<Foldout>("action-list-header__foldout");
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
 
        public void Populate(ActionList actionList)
        {
            this.actionList = actionList;
            listView.itemsSource = actions;
            listView.bindingPath = "actions";
        }

        public void SetHeaderTitle(string newTitle)
        {
            headerTitle = newTitle;
            headerFoldout.text = newTitle;
        }
            
        private void RegisterCallbacks()
        {
            RegisterCallback<AttachToPanelEvent>(e => SetHeaderTitle(headerTitle));

            headerFoldout.RegisterValueChangedCallback(evt =>
            {
                if (evt.target == headerFoldout)
                {
                    ToggleFoldout();
                }
            });
            
            listView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(() =>
            {
                BlackboardActionSearchWindow.Open(AddAction);
            });
            
            listView.Q<Button>("unity-list-view__remove-button").clickable = new Clickable(() =>
            {
                if(listView.selectedIndex != -1)
                    RemoveAction(actionList.actions[listView.selectedIndex]);
            });
        }

        private void ToggleFoldout()
        {
            foldoutExpanded = !foldoutExpanded;
            
            if (foldoutExpanded)
            {
                listView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }
            else
            {
                listView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }
        }
        
        protected virtual void AddAction(Type actionType)
        {
            var newAction = ScriptableObject.CreateInstance(actionType) as Action;
            actions.Add(newAction);
        
            listView.RefreshItems();
        }

        protected virtual void RemoveAction(Action action)
        {
            actions.Remove(action);
        
            listView.RefreshItems();
        }
        
        private VisualElement MakeItem() => new ActionView();

        private void BindItem(VisualElement element, int i)
        {
            var actionView = element as ActionView;
    
            actionView.SetAction(actions[i]);
            actionView.onActionSelected = (newAction => OnActionTypeChanged(newAction, i));
        }
        
        protected virtual void OnActionTypeChanged(Action newAction, int i)
        {
            actions[i] = newAction;
        
            listView.RefreshItems();       
        }
    }

    public class ActionListUxmlTraits : VisualElement.UxmlTraits
    {
        private UxmlStringAttributeDescription headerTitle = new() { name = "header-title", defaultValue = "Actions"}; 
        
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var element = ve as ActionListView;

            element.headerTitle = headerTitle.GetValueFromBag(bag, cc);
        }
    }
}
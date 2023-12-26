using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Actions
{
    public class ActionDropdown : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ActionDropdown, UxmlTraits>{}

        private const string UxmlPath = "UXML/ElementDropdown.uxml";
        
        public Action<Action> onActionSelected;

        public Action actionSelected;

        private Button buttonPopup;

        public ActionDropdown()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            buttonPopup = this.Q<Button>();
            buttonPopup.clickable = new Clickable(() =>
            {
                BlackboardActionSearchWindow.Open(OnActionSelected);
            });
        }

        private void OnActionSelected(Type actionTypeSelected)
        {
            if (actionSelected != null && actionSelected.GetType() == actionTypeSelected)
                return;

            var newActionSelected = ScriptableObject.CreateInstance(actionTypeSelected) as Action;

            SetAction(newActionSelected);
        }

        public void SetAction(Action newActionSelected)
        {
            actionSelected = newActionSelected;

            UpdateButtonText();

            onActionSelected?.Invoke(actionSelected);
        }

        public void UpdateButtonText()
        {
            if(actionSelected != null)
                buttonPopup.text = actionSelected.GetName();
        }
    }
}
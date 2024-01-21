using System;
using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventDropdown : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventDropdown, UxmlTraits>{}
        
        private const string UxmlPath = "UXML/ElementDropdown.uxml";
        
        public Action<BaseEventSO> onEventSelected;

        public BaseEventSO eventSelected;

        private Button buttonPopup;

        public EventDropdown()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            buttonPopup = this.Q<Button>();
            buttonPopup.clickable = new Clickable(() =>
            {
                EventSearchWindow.Open(OnEventSelected);
            });
        }

        private void OnEventSelected(Type eventTypeSelected)
        {
            if (eventSelected != null && eventSelected.GetType() == eventTypeSelected)
                return;

            var newEventSelected = ScriptableObject.CreateInstance(eventTypeSelected) as BaseEventSO;

            SetEvent(newEventSelected);
        }

        public void SetEvent(BaseEventSO newEventSelected)
        {
            eventSelected = newEventSelected;

            UpdateButtonText();

            onEventSelected?.Invoke(eventSelected);
        }

        public void UpdateButtonText()
        {
            if (eventSelected != null)
                buttonPopup.text = eventSelected.GetName();
        }
    }    
}

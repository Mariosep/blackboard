using System;
using System.IO;
using Blackboard.Editor.Events;
using Blackboard.Events;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class EventConditionView : VisualElement
    {
        private readonly string uxmlName = "UXML/Event.uxml";

        public Action<BaseEventSO> onEventSelected;

        private EventDropdown eventDropdown;
        //private VisualElement 

        public EventConditionView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
        
        
        }
    }
}
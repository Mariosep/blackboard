using System.Collections.Generic;
using System.IO;
using Blackboard.Events;
using Blackboard.Utils.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventSimulatorEditorWindow : EditorWindow
    {
        public static string RelativePath => AssetDataBaseExtensions.GetDirectoryOfScript<EventSimulatorEditorWindow>();
        private readonly string uxmlPath = "UXML/EventSimulatorEditor.uxml";

        // Data
        private BlackboardSO _blackboard;
        private EventDataBase _eventDataBase;
        private EventChannelInfo _eventChannelInfo;
        
        // Visual elements
        private EventGroupSelectorView _groupSelectorView;
        private EventsSimulatorView _eventsSimulatorView;
        
        [MenuItem("Tools/Event Simulator")]
        public static void OpenWindow()
        {
            EventSimulatorEditorWindow wnd = GetWindow<EventSimulatorEditorWindow>("Event Simulator");
            wnd.minSize = new Vector2(650, 300);
        }
    
        public void CreateGUI()
        {
            string path = Path.Combine(RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(rootVisualElement);

            _blackboard = BlackboardEditorManager.instance.Blackboard;
            _eventDataBase = _blackboard.EventDataBase;
            
            _groupSelectorView = rootVisualElement.Q<EventGroupSelectorView>();
            _eventsSimulatorView = rootVisualElement.Q<EventsSimulatorView>();
            
            RegisterCallbacks();
            
            PopulateView(_eventDataBase);
        }
        
        private void PopulateView(EventDataBase eventDataBase)
        {
            this._eventDataBase = eventDataBase;
            List<string> categories = eventDataBase.Categories;
            
            _groupSelectorView.PopulateView(categories);
        
            if (categories.Count == 0)
                _eventsSimulatorView.visible = false;
        }
        
        private void RegisterCallbacks()
        {
            _groupSelectorView.onCategorySelected += OnCategorySelected;
        }

        private void OnCategorySelected(string category)
        {
            var eventChannelInfo = _eventDataBase.GetEventChannelFromCategory(category);
        
            if(eventChannelInfo != null)
                ShowGroup(eventChannelInfo);
            else
                HideItemView();
        }
    
        private void ShowGroup(EventChannelInfo eventChannelInfo)
        {
            _eventChannelInfo = eventChannelInfo;
            _eventsSimulatorView.PopulateView(eventChannelInfo);
            _eventsSimulatorView.visible = true;
        }
    
        private void HideItemView()
        {
            _eventsSimulatorView.visible = false;
        }
    }
}

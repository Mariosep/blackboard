using System;
using System.Collections.Generic;
using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventsLogView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventsLogView, UxmlTraits> { }
        
        private const string UxmlPath = "UXML/EventsLog.uxml";

        // Data
        private List<string> eventsInvoked;
        
        // Visual elements
        private ListView _listView;
        
        public EventsLogView()
        {
            string path = Path.Combine(EventSimulatorEditorWindow.RelativePath, UxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
            
            _listView = this.Q<ListView>();
            
            Setup();
        }

        private void Setup()
        {
            eventsInvoked = new List<string>();
            _listView.itemsSource = eventsInvoked;
            
            RegisterCallbacks();
        }

        private void OnEventInvoked(EventInvocationInfo eventData)
        {
            DateTime currentTime = DateTime.Now;
            string formattedTime = currentTime.ToString("HH:mm:ss:fff");

            string eventLog = $"[{formattedTime}]: {eventData.EventName}";
            
            eventsInvoked.Add(eventLog);
            _listView.RefreshItems();
        }
        
        private void RegisterCallbacks()
        {
            if (Application.isPlaying)
                EventChannel.onEventInvoked += OnEventInvoked;
            
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
            UnregisterCallback<AttachToPanelEvent>(_ => RegisterCallbacks());
        }

        private void UnregisterCallbacks()
        {
            if (Application.isPlaying)
                EventChannel.onEventInvoked -= OnEventInvoked;
            
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            RegisterCallback<AttachToPanelEvent>(_ => RegisterCallbacks());
        }
        
        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                EventChannel.onEventInvoked += OnEventInvoked;
            }
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                EventChannel.onEventInvoked -= OnEventInvoked;
            }
        }
    }
}
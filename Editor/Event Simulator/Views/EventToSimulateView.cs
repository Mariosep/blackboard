using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventToSimulateView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventToSimulateView, UxmlTraits>{}
        
        private readonly string uxmlPath = "UXML/EventToSimulate.uxml";

        // Data
        private EventChannelInfo eventChannelInfo;
        private BaseEventSO eventData;

        // Visual elements
        private Label eventNameLabel;
        private VisualElement argumentsContainer;
        private Button invokeButton;

        public EventToSimulateView()
        {
            string path = Path.Combine(EventSimulatorEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            eventNameLabel = this.Q<Label>("event-name");
            argumentsContainer = this.Q<VisualElement>("arguments-container");
            invokeButton = this.Q<Button>("invoke");

            invokeButton.clicked += Invoke;
        }

        public void PopulateView(EventChannelInfo eventChannelInfo, EventInfo eventInfo)
        {
            argumentsContainer.Clear();

            this.eventChannelInfo = eventChannelInfo;
            eventNameLabel.text = eventInfo.eventName;

            eventData = ScriptableObject.CreateInstance(eventInfo.eventType) as BaseEventSO;
            eventData.Init(GUID.Generate().ToString());
            var serializedEvent = new SerializedObject(eventData);

            foreach (ParameterInfo argument in eventInfo.parameters)
            {
                SerializedProperty argumentProperty = serializedEvent.FindProperty(argument.parameterName);
                var propertyField = new PropertyField(argumentProperty);
                propertyField.name = argument.parameterName;
                propertyField.userData = argumentProperty;
                propertyField.bindingPath = argument.parameterName;
                argumentsContainer.Add(propertyField);
            }
            
            argumentsContainer.Bind(serializedEvent);
        }

        private void Invoke()
        {
            if (Application.isPlaying)
            {
                var getMethod = typeof(ServiceLocator).GetMethod("Get")?.MakeGenericMethod(eventChannelInfo.channelType);
                object result = getMethod.Invoke(null, null);

                EventChannel eventChannel = (EventChannel)result;
                eventChannel.InvokeEvent(eventData);
            }
        }
    }
}
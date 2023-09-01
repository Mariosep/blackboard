using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class EventsView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<EventsView, UxmlTraits> { }
    
    private readonly string uxmlPath = "UXML/Events.uxml";
    
    private EventGroupSO _eventGroup;
    private bool _simulatorModeEnabled;
    
    private GroupHeaderView _groupHeader;
    private VisualElement _eventsContainer;
    private EventsEditorView _eventsEditorView;
    private EventsSimulatorView _eventsSimulatorView;

    private Button _simulatorModeButton;
    
    public EventsView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        _groupHeader = this.Q<GroupHeaderView>();
        _eventsContainer = this.Q<VisualElement>("events__container");
        _simulatorModeButton = this.Q<Button>("simulator-mode__button");
        
        RegisterCallbacks();
    }
    
    private void RegisterCallbacks()
    {
        _simulatorModeButton.clicked += ToggleSimulatorMode;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _simulatorModeButton.clicked -= ToggleSimulatorMode;
    }
    
    public void PopulateView(EventGroupSO eventGroup)
    {
        _eventGroup = eventGroup;
        
        _groupHeader.SetGroup(_eventGroup, BlackboardElementType.Event);
        
        if (_simulatorModeEnabled)
            ShowEventsSimulator();
        else
            ShowEventsEditor();
    }

    private void ToggleSimulatorMode()
    {
        _simulatorModeEnabled = !_simulatorModeEnabled;
        
        if (_simulatorModeEnabled)
        {
            ShowEventsSimulator();

            _simulatorModeButton.text = "Disable Simulator Mode";
        }
        else
        {
            ShowEventsEditor();
            
            _simulatorModeButton.text = "Enable Simulator Mode";
        }
    }

    private void ShowEventsEditor()
    {
        _eventsContainer.Clear();
        
        _eventsEditorView = new EventsEditorView();
        _eventsEditorView.PopulateView(_eventGroup);
        
        _eventsContainer.Insert(0, _eventsEditorView);
    }
    
    private void ShowEventsSimulator()
    {
        _eventsContainer.Clear();
        
        _eventsSimulatorView = new EventsSimulatorView();
        _eventsSimulatorView.PopulateView(_eventGroup);
        
        _eventsContainer.Insert(0, _eventsSimulatorView);
    }
}
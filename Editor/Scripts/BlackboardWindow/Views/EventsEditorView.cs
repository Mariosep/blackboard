using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

public class EventsEditorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<EventsEditorView, UxmlTraits> { }
    
    private readonly string uxmlPath = "UXML/EventsEditor.uxml";
    
    private EventGroupSO _eventGroup;
    
    private EventsListView _evensListView;
    private Button _addEventButton;
    private Button _removeEventButton;

    public EventsEditorView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        _addEventButton = this.Q<Button>("add-event__button");
        _removeEventButton = this.Q<Button>("remove-event__button");

        RegisterCallbacks();
    }
    
    private void RegisterCallbacks()
    {
        _addEventButton.clicked += OnAddEventButtonClicked;
        _removeEventButton.clicked += OnRemoveEventButtonClicked;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _addEventButton.clicked -= OnAddEventButtonClicked;
        _removeEventButton.clicked -= OnRemoveEventButtonClicked;
        
        if(_evensListView != null)
            _evensListView.onEventTypeChanged += OnEventTypeChanged;
    }
    
    public void PopulateView(EventGroupSO eventGroup)
    {
        _eventGroup = eventGroup;
        
        if(_evensListView != null)
            Remove(_evensListView);

        _evensListView = new EventsListView();
        _evensListView.Populate(_eventGroup);
        
        _evensListView.onEventTypeChanged += OnEventTypeChanged;
        
        Insert(0, _evensListView);
    }
    
    private void AddEvent()
    {
        EventSO newEvent = BlackboardElementFactory.CreateEvent(BlackboardEventType.Actor);
        ScriptableObjectUtility.SaveSubAsset(newEvent, BlackboardManager.instance.Blackboard);
        
        _evensListView.Add(newEvent);
        
        EditorUtility.SetDirty(_eventGroup);
        AssetDatabase.SaveAssets();
    }

    private void RemoveEvent(params EventSO[] events)
    {
        _evensListView.Remove(events);
        ScriptableObjectUtility.DeleteSubAsset(events);
        
        EditorUtility.SetDirty(_eventGroup);
        AssetDatabase.SaveAssets();
    }
    
    private void OnAddEventButtonClicked()
    {
        AddEvent();
    }
    
    private void OnRemoveEventButtonClicked()
    {
        EventSO[] eventsSelected = _evensListView.eventsSelected;
        
        if(eventsSelected.Length == 0 && _eventGroup.elementsList.Count > 0)
            eventsSelected = new [] { _eventGroup.elementsList.Last() };
        
        if(eventsSelected.Length > 0)
        {
            ShowConfirmEventDeletionPopUp(eventsSelected);
        }
    }

    private void OnEventTypeChanged(int i, BlackboardEventType newType)
    {
        EventSO eventToReplace = _eventGroup.elementsList[i];

        if(newType == eventToReplace.type)
            return;
        
        EventSO newEvent = BlackboardElementFactory.CreateEvent(newType, eventToReplace.id);
        newEvent.groupId = eventToReplace.groupId;
        newEvent.theName = eventToReplace.theName;

        ScriptableObjectUtility.DeleteSubAsset(eventToReplace);
        ScriptableObjectUtility.SaveSubAsset(newEvent, BlackboardManager.instance.Blackboard);

        _evensListView.Replace(i, newEvent);
    }
    
    private void ShowConfirmEventDeletionPopUp(params EventSO[] events)
    {
        bool deleteClicked = EditorUtility.DisplayDialog(
            "Delete event selected?",
            "Are you sure you want to delete this event",
            "Delete", 
            "Cancel");

        if (deleteClicked)
        {
            RemoveEvent(events);
        }
    }
}
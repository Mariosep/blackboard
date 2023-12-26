using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class EventSectionView : VisualElement
{
    private readonly string uxmlPath = "UXML/EventSection.uxml";
    
    private EventDataBaseSO _eventDataBase;
    private EventGroupSO _eventGroupSelected;
    
    private GroupSelectorView _groupSelectorView;
    private EventsView _eventsView;

    public EventSectionView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        _groupSelectorView = this.Q<GroupSelectorView>();
        _eventsView = this.Q<EventsView>();
        
        RegisterCallbacks();
    }

    public void PopulateView(EventDataBaseSO eventDataBase)
    {
        _eventDataBase = eventDataBase;
        
        _groupSelectorView.PopulateView(_eventDataBase);
        
        if (eventDataBase.groupsList.Count == 0)
            _eventsView.visible = false;
    }

    private void RegisterCallbacks()
    {
        _groupSelectorView.onGroupSelected += OnGroupSelected;
        _groupSelectorView.onGroupListChanged += OnGroupListChanged;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _groupSelectorView.onGroupSelected -= OnGroupSelected;
        _groupSelectorView.onGroupListChanged -= OnGroupListChanged;
    }

    private void OnGroupSelected(int groupIndex)
    {
        if(groupIndex != -1 && _eventDataBase.groupsList.Count > groupIndex)
            ShowGroup(_eventDataBase.groupsList[groupIndex]);
        else
            HideItemView();
    }
    
    private void OnGroupListChanged()
    {
        OnGroupSelected(_groupSelectorView.GroupIndexSelected);
    }
    
    private void ShowGroup(EventGroupSO eventGroup)
    {
        if (_eventGroupSelected != null)
        {
            EditorUtility.SetDirty(_eventGroupSelected);
            AssetDatabase.SaveAssets();
        }
        
        if (eventGroup != null)
        {
            _eventGroupSelected = eventGroup;
            
            _eventsView.PopulateView(eventGroup);
            _eventsView.visible = true;
        }
    }
    
    private void HideItemView()
    {
        _eventsView.visible = false;
    }
}
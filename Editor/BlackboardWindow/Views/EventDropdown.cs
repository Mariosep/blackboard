using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class EventDropdown : VisualElement
{
    private const string UxmlPath = "UXML/ElementDropdown.uxml";
    
    public Action<EventSO> onEventSelected;
    public Action<BlackboardElementSO> onArgSelected;
    
    public EventSO eventSelected;
    public EventType eventType;

    public BlackboardElementSO argSelected;
    
    private bool filterByEventType = false;
    
    private Label nameLabel;
    private VisualElement content;
    private Button buttonPopup;

    public EventDropdown()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        nameLabel = this.Q<Label>("name-label");
        
        content = this.Q<VisualElement>("content");
        
        buttonPopup = this.Q<Button>();
        buttonPopup.clicked += OpenSearchWindow;
        
        UpdateButtonText();
    }
    
    public void SetName(string factName)
    {
        nameLabel.text = factName;
        nameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }
    
    public void SetEventType(EventType eventType, bool showEventArg = false)
    {
        this.eventType = eventType;
        filterByEventType = true;
        
        if(showEventArg)
            ShowEventArg();
    }
    
    private void ShowEventArg()
    {
        switch (eventType)
        {
            case EventType.Actor:
                var actorDropdown = new ActorDropdown();
                actorDropdown.onActorSelected += SetArg;
        
                actorDropdown.FitSize();
        
                if(argSelected != null)
                    actorDropdown.SetActor((ActorSO) argSelected);
                
                content.Add(actorDropdown);
                break;
            
            case EventType.Item:
                var itemDropdown = new ItemDropdown();
                itemDropdown.onItemSelected += SetArg;
        
                itemDropdown.FitSize();
        
                if(argSelected != null)
                    itemDropdown.SetItem((ItemSO) argSelected);
                
                content.Add(itemDropdown);
                break;
        }
    }
    
    private void OpenSearchWindow()
    {
        var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        
        var eventPairs = GetEventPairs();

        var eventSearchProvider = ScriptableObject.CreateInstance<EventSearchProvider>();
        eventSearchProvider.Init(eventPairs, SetEvent);
        
        SearchWindow.Open(new SearchWindowContext(mousePos), eventSearchProvider);
    }
    
    private List<KeyValuePair<EventSO, string>> GetEventPairs()
    {
        if(filterByEventType)
            return BlackboardEditorManager.instance.EventDataBase.GetPairs(eventType);
        else
            return BlackboardEditorManager.instance.EventDataBase.GetPairs();
    }
    
    public void BindEvent(EventSO eventSo)
    {
        if(eventSelected != null)
            eventSelected.onNameChanged -= UpdateButtonText;
        
        eventSelected = eventSo;

        eventSelected.onNameChanged += UpdateButtonText;
    }
    
    public void SetEvent(EventSO newEventSelected)
    {
        if(eventSelected == newEventSelected)
            return;

        BindEvent(newEventSelected);
        
        UpdateButtonText();
        
        onEventSelected?.Invoke(newEventSelected);
    }
    
    public void SetArg(BlackboardElementSO newArgSelected)
    {
        if (argSelected == newArgSelected)
            return;

        argSelected = newArgSelected;

        onArgSelected?.Invoke(newArgSelected);
    }
    
    private void UpdateButtonText()
    {
        string eventPath = "";

        if (eventSelected != null)
            eventPath = BlackboardEditorManager.instance.GetEventPath(eventSelected.groupId, eventSelected.id);
        
        buttonPopup.text = eventPath;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class EventDropdown : VisualElement
{
    private const string UxmlPath = "UXML/EventDrawer.uxml";
    
    public Action<EventSO> onEventSelected;
    public Action<BlackboardElementSO> onArgSelected;
    
    public EventSO eventSelected;
    public BlackboardEventType eventType;

    public BlackboardElementSO argSelected;
    
    private bool filterByEventType = false;
    
    private Label nameLabel;
    private VisualElement dropdowns;
    private Button buttonPopup;

    public EventDropdown()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        nameLabel = this.Q<Label>("name-label");
        
        dropdowns = this.Q<VisualElement>("dropdowns");
        
        buttonPopup = this.Q<Button>();
        buttonPopup.clicked += OpenSearchWindow;
        
        UpdateButtonText();
    }
    
    public void SetName(string factName)
    {
        nameLabel.text = factName;
        nameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }
    
    public void SetEventType(BlackboardEventType eventType)
    {
        this.eventType = eventType;
        filterByEventType = true;
        
        ShowEventArg();
    }
    
    private void ShowEventArg()
    {
        switch (eventType)
        {
            case BlackboardEventType.Actor:
                var actorDropdown = new ActorDropdown();
                actorDropdown.onActorSelected += SetArg;
        
                actorDropdown.FitSize();
        
                if(argSelected != null)
                    actorDropdown.SetActor((ActorSO) argSelected);
                
                dropdowns.Add(actorDropdown);
                break;
            
            case BlackboardEventType.Item:
                var itemDropdown = new ItemDropdown();
                itemDropdown.onItemSelected += SetArg;
        
                itemDropdown.FitSize();
        
                if(argSelected != null)
                    itemDropdown.SetItem((ItemSO) argSelected);
                
                dropdowns.Add(itemDropdown);
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
            return BlackboardManager.instance.EventDataBase.GetPairs(eventType);
        else
            return BlackboardManager.instance.EventDataBase.GetPairs();
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
            eventPath = BlackboardManager.instance.GetEventPath(eventSelected.groupId, eventSelected.id);
        
        buttonPopup.text = eventPath;
    }
}
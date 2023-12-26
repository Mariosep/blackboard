using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class EventsSimulatorListView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<EventsSimulatorListView, UxmlTraits> { }

    private readonly string uxmlPath = "UXML/EventsSimulatorListView.uxml";

    public Action<int, EventType> onEventTypeChanged; 
    
    private MultiColumnListView _listView;
    private EventGroupSO _eventGroup;
    private SerializedProperty _eventsProperty;
    private List<EventSO> _events => _eventGroup.elementsList;
    private List<ScriptableObject> _arguments;

    public EventSO[] eventsSelected => _listView.selectedItems.Cast<EventSO>().ToArray();
    
    public EventsSimulatorListView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);
        
        _listView = this.Q<MultiColumnListView>();
        
        Setup();
    }

    private void Setup()
    {
        _listView.columns["name"].makeCell = MakeNameCell;
        _listView.columns["argument"].makeCell = MakeArgumentCell;
        _listView.columns["type"].makeCell = MakeTypeCell;
        _listView.columns["invoke"].makeCell = MakeInvokeCell;
        
        _listView.columns["name"].bindCell = (element, i) => BindName(element, new SerializedObject(_events[i]));
        _listView.columns["argument"].bindCell = (element, i) => BindArgument(element, i);
        _listView.columns["type"].bindCell = (element, i) => BindType(element, i);
        _listView.columns["invoke"].bindCell = (element, i) => BindInvoke(element, i);
    }
    
    public void Populate(EventGroupSO eventGroup)
    {
        _eventGroup = eventGroup;

        _arguments = new List<ScriptableObject>();
        for (int i = 0; i < _events.Count; i++)
            _arguments.Add(null);
        
        _listView.itemsSource = _events;
        _listView.RefreshItems();
    }

    #region Modify list
    public void Add(EventSO eventSO)
    {
        _eventGroup.AddElement(eventSO);
        _listView.RefreshItems();
    }
    
    public void Remove(params EventSO[] events)
    {
        foreach (EventSO eventSo in events)
            _eventGroup.RemoveElement(eventSo.id);    
        
        _listView.RefreshItems();
    }

    public void Replace(int i, EventSO eventSo)
    {
        _eventGroup.Replace(i, eventSo);
        _listView.RefreshItem(i);
    }
    #endregion

    #region Make
    private VisualElement MakeCell()
    {
        var cell = new VisualElement();
        cell.AddToClassList("centered-vertical");
        cell.style.paddingTop = 5f;
        cell.style.paddingBottom = 5f;
        
        return cell;
    }
    
    private VisualElement MakeNameCell()
    {
        var cell = MakeCell();
        
        var nameLabel = new Label("");
        nameLabel.bindingPath = "theName";
        cell.Add(nameLabel);

        return cell;
    }
    
    private VisualElement MakeArgumentCell()
    {
        var cell = MakeCell();
        return cell;
    }
    
    private VisualElement MakeTypeCell()
    {
        var cell = MakeCell();
        
        var typeLabel = new Label("");
        cell.Add(typeLabel);

        return cell;
    }
    
    private VisualElement MakeInvokeCell()
    {
        var cell = MakeCell();

        var invokeButton = new Button();
        invokeButton.text = "Invoke";
        
        cell.Add(invokeButton);

        return cell;
    }
    #endregion
    
    #region Bind
    private void BindName(VisualElement cell, SerializedObject serializedObject)
    {
        Label nameLabel = cell.Q<Label>();
        nameLabel.Bind(serializedObject);
    }
    
    private void BindArgument(VisualElement cell, int i)
    {
        switch (_events[i].type)
        {
            case EventType.Actor:
                var actorDropdown = new ActorDropdown();
                cell.Add(actorDropdown);
                break;
            
            case EventType.Item:
                var itemDropdown = new ItemDropdown();
                cell.Add(itemDropdown);
                break;
        }
    }
    
    private void BindType(VisualElement cell, int i)
    {
        var typeLabel = cell.Q<Label>();
        
        switch (_events[i].type)
        {
            case EventType.Actor:
                typeLabel.text = "Actor";
                break;
            
            case EventType.Item:
                typeLabel.text = "Item";
                break;
        }
    }
    
    private void BindInvoke(VisualElement cell, int i)
    {
        var invokeButton = cell.Q<Button>();
        
        invokeButton.clicked += () => OnInvokeClicked(i);
    }
    #endregion

    private void OnInvokeClicked(int i)
    {
        EventSO eventSo = _events[i];
        //ScriptableObject argumentSo = _arguments[i];
        
        var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
        var itemView = list.ElementAt(i);

        if (eventSo is EventItemSO eventItemSo)
        {
            var itemDropdown = itemView.Q<ItemDropdown>();
            
            eventItemSo.Invoke(itemDropdown.itemSelected);
        }
        else if (eventSo is EventActorSO eventActorSo)
        {
            var actorDropdown = itemView.Q<ActorDropdown>();
            
            eventActorSo.Invoke(actorDropdown.actorSelected);
        }
    }
}
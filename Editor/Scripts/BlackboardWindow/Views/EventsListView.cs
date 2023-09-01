using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class EventsListView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<EventsListView, UxmlTraits> { }

    private readonly string uxmlPath = "UXML/EventsListView.uxml";

    public Action<int, BlackboardEventType> onEventTypeChanged; 
    
    private MultiColumnListView _listView;
    private EventGroupSO _eventGroup;
    private SerializedProperty _eventsProperty;
    private List<EventSO> _events => _eventGroup.elementsList;

    public EventSO[] eventsSelected => _listView.selectedItems.Cast<EventSO>().ToArray();
    
    public EventsListView()
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
        _listView.columns["description"].makeCell = MakeDescriptionCell;
        _listView.columns["type"].makeCell = MakeTypeCell;
        
        _listView.columns["name"].bindCell = (element, i) => BindName(element, _events[i]);
        _listView.columns["description"].bindCell = (element, i) => BindDescription(element, new SerializedObject(_events[i]));
        _listView.columns["type"].bindCell = (element, i) => BindType(element, i);
    }
    
    public void Populate(EventGroupSO eventGroup)
    {
        _eventGroup = eventGroup;
        _listView.itemsSource = _events;
        _listView.RefreshItems();
    }

    #region Modify list
    public void Add(EventSO eventSO)
    {
        _eventGroup.AddElement(eventSO);
        _listView.RefreshItems();
        
        int lastIndex = _eventGroup.elementsList.Count - 1;
        
        _listView.SetSelection(lastIndex);
        var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
        var itemView = list.ElementAt(lastIndex);
        var nameField = itemView.Q<TextField>("name-field");
        nameField.Focus();
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
        cell.AddToClassList("centered");
        cell.style.paddingTop = 5f;
        cell.style.paddingBottom = 5f;
        
        return cell;
    }
    
    private VisualElement MakeNameCell()
    {
        var cell = MakeCell();
        
        var nameField = new TextField("");
        nameField.name = "name-field";
        nameField.bindingPath = "theName";
        cell.Add(nameField);

        return cell;
    }
    
    private VisualElement MakeDescriptionCell()
    {
        var cell = MakeCell();
        
        var descriptionField = new TextField("");
        descriptionField.name = "description-field";
        descriptionField.bindingPath = "description";
        
        cell.Add(descriptionField);

        return cell;
    }
    
    private VisualElement MakeTypeCell()
    {
        var cell = MakeCell();
        
        var enumField = new EnumField(BlackboardEventType.Item);
        cell.Add(enumField);

        return cell;
    }
    #endregion
    
    #region Bind
    private void BindName(VisualElement cell, EventSO eventSo)
    {
        TextField nameField = cell.Q<TextField>();
        nameField.value = eventSo.theName;
        
        nameField.RegisterCallback<FocusOutEvent>(e =>
        {
            bool isValid = BlackboardValidator.ValidateElementName(nameField.value, eventSo.theName);
            
            if(isValid)
                eventSo.SetName(nameField.value);
            else
                nameField.value = eventSo.theName;
        });
    }
    
    // TODO: Extract bind and make methods to avoid repeated code with the rest of blackboard elements
    private void BindDescription(VisualElement cell, SerializedObject serializedObject)
    {
        TextField descriptionField = cell.Q<TextField>();
        descriptionField.Bind(serializedObject);
    }
    
    private void BindType(VisualElement cell, int i)
    {
        EnumField typeField = cell.Q<EnumField>();
        
        typeField.value = _events[i].type;
        typeField.RegisterValueChangedCallback(e => onEventTypeChanged?.Invoke(i, (BlackboardEventType)e.newValue));
    }
    #endregion
}
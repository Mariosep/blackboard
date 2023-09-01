using UnityEditor;
using UnityEditor.UIElements;

public class EventGroupListView : GroupListView
{
    private EventDataBaseSO _eventDataBase;
    
    protected override void Setup()
    {
        _listView.makeItem = MakeItem;
        _listView.bindItem = (element, i) => BindItem(element, new SerializedObject(_eventDataBase.groupsList[i]));
    }
    
    public void PopulateView(EventDataBaseSO eventDataBase)
    {
        _eventDataBase = eventDataBase;
        _listView.bindingPath = "groupsList";
        _listView.Bind(new SerializedObject(_eventDataBase));
        _listView.itemsSource = _eventDataBase.groupsList;
        
        SetSelection(0);
    }
}
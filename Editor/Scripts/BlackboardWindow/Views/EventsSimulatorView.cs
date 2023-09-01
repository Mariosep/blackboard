using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

public class EventsSimulatorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<EventsSimulatorView, UxmlTraits> { }
    
    private EventGroupSO _eventGroup;
    
    private EventsSimulatorListView _eventsSimulatorListView;

    public EventsSimulatorView()
    {
        style.flexGrow = 1;
    }

    public void PopulateView(EventGroupSO eventGroup)
    {
        _eventGroup = eventGroup;
        
        if(_eventsSimulatorListView != null)
            Remove(_eventsSimulatorListView);

        _eventsSimulatorListView = new EventsSimulatorListView();
        _eventsSimulatorListView.Populate(_eventGroup);
        
        Insert(0, _eventsSimulatorListView);
    }
}
using System.Collections.Generic;

public class EventGroupSO : ElementGroupSO<EventSO>
{
    public List<KeyValuePair<EventSO, string>> GetPairs(EventType eventType)
    {
        var pairs = new List<KeyValuePair<EventSO, string>>();

        foreach (EventSO eventSo in elementsList)
        {
            if(eventSo.type == eventType)
                pairs.Add(new KeyValuePair<EventSO, string>(eventSo, $"{groupName}/{eventSo.theName}"));
        }

        return pairs;
    }
    
    public List<KeyValuePair<EventSO, string>> GetPairs()
    {
        var pairs = new List<KeyValuePair<EventSO, string>>();

        foreach (EventSO eventSo in elementsList)
        {
            pairs.Add(new KeyValuePair<EventSO, string>(eventSo, $"{groupName}/{eventSo.theName}"));
        }

        return pairs;
    }
}
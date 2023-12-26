using System;
using System.Collections.Generic;

public class EventSearchProvider : DataSearchProvider<EventSO>
{
    public override void Init(List<KeyValuePair<EventSO, string>> items, Action<EventSO> callback)
    {
        base.Init(items, callback);

        searchTreeTitle = "Events";
    }
}
using System;
using System.Collections.Generic;

public class ActorSearchProvider : DataSearchProvider<ActorSO>
{
    public override void Init(List<KeyValuePair<ActorSO, string>> items, Action<ActorSO> callback)
    {
        base.Init(items, callback);

        searchTreeTitle = "Actors";
    }
}
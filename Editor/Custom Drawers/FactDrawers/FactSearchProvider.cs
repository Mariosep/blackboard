using System;
using System.Collections.Generic;

public class FactSearchProvider : DataSearchProvider<FactSO>
{
    public override void Init(List<KeyValuePair<FactSO, string>> items, Action<FactSO> callback)
    {
        base.Init(items, callback);

        searchTreeTitle = "Facts";
    }
}
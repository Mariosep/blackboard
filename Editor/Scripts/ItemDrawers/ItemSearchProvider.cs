using System;
using System.Collections.Generic;

public class ItemSearchProvider : DataSearchProvider<ItemSO>
{
    public override void Init(List<KeyValuePair<ItemSO, string>> items, Action<ItemSO> callback)
    {
        base.Init(items, callback);

        searchTreeTitle = "Items";
    }
}
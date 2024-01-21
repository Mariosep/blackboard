using System;
using System.Collections.Generic;
using Blackboard.Items;

namespace Blackboard.Editor.Items
{
    public class ItemSearchProvider : DataSearchProvider<ItemSO>
    {
        public override void Init(List<KeyValuePair<ItemSO, string>> commands, Action<ItemSO> callback)
        {
            base.Init(commands, callback);

            searchTreeTitle = "Items";
        }
    }
}
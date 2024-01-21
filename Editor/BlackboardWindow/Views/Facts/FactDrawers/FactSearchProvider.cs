using System;
using System.Collections.Generic;
using Blackboard.Facts;

namespace Blackboard.Editor.Facts
{
    public class FactSearchProvider : DataSearchProvider<FactSO>
    {
        public override void Init(List<KeyValuePair<FactSO, string>> commands, Action<FactSO> callback)
        {
            base.Init(commands, callback);

            searchTreeTitle = "Facts";
        }
    }
}
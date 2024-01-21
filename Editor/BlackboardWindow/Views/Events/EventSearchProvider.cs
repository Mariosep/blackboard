using System;
using System.Collections.Generic;

namespace Blackboard.Editor.Events
{
    public class EventSearchProvider : DataSearchProvider<Type>
    {
        public override void Init(List<KeyValuePair<Type, string>> items, Action<Type> callback)
        {
            base.Init(items, callback);

            searchTreeTitle = "Events";
        }
    }
}
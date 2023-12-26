using System;
using System.Collections.Generic;

public class BlackboardActionSearchProvider : DataSearchProvider<Type>
{
    public override void Init(List<KeyValuePair<Type, string>> actions, Action<Type> callback)
    {
        base.Init(actions, callback);

        searchTreeTitle = "Actions";
    }
}
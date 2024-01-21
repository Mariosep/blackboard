using System;
using System.Collections.Generic;

namespace Blackboard.Editor.Commands
{
    public class CommandSearchProvider : DataSearchProvider<Type>
    {
        public override void Init(List<KeyValuePair<Type, string>> commands, Action<Type> callback)
        {
            base.Init(commands, callback);

            searchTreeTitle = "Commands";
        }
    }    
}


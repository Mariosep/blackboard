using System;
using System.Collections.Generic;
using Blackboard.Actors;

namespace Blackboard.Editor.Actors
{
    public class ActorSearchProvider : DataSearchProvider<ActorSO>
    {
        public override void Init(List<KeyValuePair<ActorSO, string>> commands, Action<ActorSO> callback)
        {
            base.Init(commands, callback);

            searchTreeTitle = "Actors";
        }
    }
}
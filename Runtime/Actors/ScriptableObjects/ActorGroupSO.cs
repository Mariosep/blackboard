using System.Collections.Generic;

namespace Blackboard.Actors
{
    public class ActorGroupSO : ElementGroupSO<ActorSO>
    {
        public List<KeyValuePair<ActorSO, string>> GetPairs()
        {
            var pairs = new List<KeyValuePair<ActorSO, string>>();

            foreach (ActorSO actor in elementsList)
            {
                pairs.Add(new KeyValuePair<ActorSO, string>(actor, $"{groupName}/{actor.Name}"));
            }

            return pairs;
        }
    }
}
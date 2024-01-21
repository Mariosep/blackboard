using System.Collections.Generic;

namespace Blackboard.Items
{
    public class ItemGroupSO : ElementGroupSO<ItemSO>
    {
        public List<KeyValuePair<ItemSO, string>> GetPairs()
        {
            var pairs = new List<KeyValuePair<ItemSO, string>>();

            foreach (ItemSO item in elementsList)
            {
                pairs.Add(new KeyValuePair<ItemSO, string>(item, $"{groupName}/{item.Name}"));
            }

            return pairs;
        }
    }
}
using System.Collections.Generic;

public class ItemGroupSO : ElementGroupSO<ItemSO>
{
    public List<KeyValuePair<ItemSO, string>> GetPairs()
    {
        var pairs = new List<KeyValuePair<ItemSO, string>>();

        foreach (ItemSO item in elementsList)
        {
            pairs.Add(new KeyValuePair<ItemSO, string>(item, $"{groupName}/{item.theName}"));
        }

        return pairs;
    }
}
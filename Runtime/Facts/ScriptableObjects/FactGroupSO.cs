using System.Collections.Generic;

namespace Blackboard.Facts
{
    public class FactGroupSO : ElementGroupSO<FactSO>
    {
        /*public bool? GetBool(string id)
    {
        if (factsDic.TryGetValue(id, out FactSO fact))
        {
            if (fact is BoolFactSO bfact)
                return bfact.Value;
        }

        return null;
    }
    
    public void SetBool(string id, bool newValue)
    {
        if (factsDic.TryGetValue(id, out FactSO fact))
        {
            if (fact is BoolFactSO bfact)
                bfact.Value = newValue;
        }
        else
            throw new Exception("Can't set bool because fact is not registered in the blackboard");
    }*/
    
        public List<KeyValuePair<FactSO, string>> GetPairs(FactType factType)
        {
            var pairs = new List<KeyValuePair<FactSO, string>>();

            foreach (FactSO fact in elementsList)
            {
                if (fact.type == factType)
                    pairs.Add(new KeyValuePair<FactSO, string>(fact, $"{groupName}/{fact.Name}"));
            }

            return pairs;
        }
    
        public List<KeyValuePair<FactSO, string>> GetPairs()
        {
            var pairs = new List<KeyValuePair<FactSO, string>>();

            foreach (FactSO fact in elementsList)
            {
                pairs.Add(new KeyValuePair<FactSO, string>(fact, $"{groupName}/{fact.Name}"));
            }

            return pairs;
        }
    }
}
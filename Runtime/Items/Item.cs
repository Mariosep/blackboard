using UnityEngine;

namespace Blackboard.Items
{
    public class Item : MonoBehaviour
    {
        public ItemSO itemData;

        public ItemSO Data => itemData;
    
        public string Name => itemData.Name;
    }
}
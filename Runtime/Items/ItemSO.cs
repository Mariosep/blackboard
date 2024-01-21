using UnityEngine;

namespace Blackboard.Items
{
    public class ItemSO : BlackboardElementSO
    {
        public GameObject prefab;
        public Texture2D icon;

        public override BlackboardElementType BlackboardElementType => BlackboardElementType.Item;
        
        public override void Init(string id)
        {
            this.id = id;
            name = $"item-{this.id}";
            theName = "NewItem";
            hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
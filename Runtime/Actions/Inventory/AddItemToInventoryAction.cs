using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("Inventory")]
    public class AddItemToInventoryAction : Action
    {
        public Item item;

        public override string GetName() => "Add Item To Inventory";
    
        public override IEnumerator Execute()
        {
            Debug.Log(GetName());
            
            yield return null;
        }
    }
}


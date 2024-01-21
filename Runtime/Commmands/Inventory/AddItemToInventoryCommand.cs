using System.Collections;
using Blackboard.Items;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Inventory")]
    public class AddItemToInventoryCommand : Command
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


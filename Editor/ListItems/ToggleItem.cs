using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class ToggleItem : BindableItem<Toggle>
    {
        public Toggle toggle;
        
        public ToggleItem(string bindingPath) : base(bindingPath, "UXML/Items/ToggleItem.uxml")
        {
            toggle = this.Q<Toggle>();
        }
    }
}
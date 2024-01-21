using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class IntegerFieldItem : BindableItem<IntegerField>
    {
        public IntegerField integerField;
        
        public IntegerFieldItem(string bindingPath) : base(bindingPath, "UXML/Items/IntegerFieldItem.uxml")
        {
            integerField = this.Q<IntegerField>();
        }
    }
}
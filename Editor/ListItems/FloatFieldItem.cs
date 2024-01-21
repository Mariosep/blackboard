using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class FloatFieldItem : BindableItem<FloatField>
    {
        public FloatField floatField;

        public FloatFieldItem(string bindingPath) : base(bindingPath, "UXML/Items/FloatFieldItem.uxml")
        {
            floatField = this.Q<FloatField>();
        }
    }
}
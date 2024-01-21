using System;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class TextFieldItem : BindableItem<TextField>
    {
        public TextField textField;
        
        public TextFieldItem(string bindingPath) : base(bindingPath, "UXML/Items/TextFieldItem.uxml")
        {
            textField = this.Q<TextField>();
        }
    }

    public class EnumFieldItem : BindableItem<EnumField>
    {
        public EnumField enumField;
        
        public EnumFieldItem(string bindingPath, Enum defaultValue) : base(bindingPath, "UXML/Items/EnumFieldItem.uxml")
        {
            var container = this.Q<VisualElement>("content-container");
            enumField = new EnumField(defaultValue);
            
            container.Clear();
            container.Add(enumField);
        }
    }
}
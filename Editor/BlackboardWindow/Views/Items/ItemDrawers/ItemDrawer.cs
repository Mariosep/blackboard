using Blackboard.Items;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Items
{
    [CustomPropertyDrawer(typeof(ItemSO))]
    public class ItemDrawer : PropertyDrawer
    {
        private SerializedProperty itemProperty;
    
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            itemProperty = property;
        
            var itemDropdown = new ItemDropdown();
            itemDropdown.SetName(property.name.Capitalize());
        
            if(itemProperty.objectReferenceValue != null)
                itemDropdown.SetItem((ItemSO)itemProperty.objectReferenceValue);
        
            itemDropdown.onItemSelected += SetItem;
        
            itemDropdown.style.marginTop = 5;
            itemDropdown.style.marginBottom = 10;

            return itemDropdown;
        }
    
        private void SetItem(ItemSO itemSelected)
        {
            itemProperty.objectReferenceValue = itemSelected;
            itemProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Item))]
public class ItemDrawer : PropertyDrawer
{
    private SerializedProperty itemProperty;
    
    private SerializedProperty idProperty;
    private SerializedProperty categoryIdProperty;
    private SerializedProperty itemSOProperty;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Get properties
        itemProperty = property;
        idProperty = property.FindPropertyRelative("id");
        categoryIdProperty = property.FindPropertyRelative("categoryId");
        itemSOProperty = property.FindPropertyRelative("itemData");

        var itemSo = (ItemSO) itemSOProperty.objectReferenceValue;
        
        var itemDropdown = new ItemDropdown();
        itemDropdown.SetName(property.name.Capitalize());
        
        if(itemSo != null)
            itemDropdown.SetItem(itemSo);

        itemDropdown.onItemSelected += SetItem;

        itemDropdown.style.marginTop = 5;
        itemDropdown.style.marginBottom = 10;
        
        return itemDropdown;
    }
    
    private void SetItem(ItemSO itemSelected)
    {
        idProperty.stringValue = itemSelected.id;
        categoryIdProperty.stringValue = itemSelected.groupId;
        
        itemSOProperty.objectReferenceValue = itemSelected;
        
        itemProperty.serializedObject.ApplyModifiedProperties();
    }
}
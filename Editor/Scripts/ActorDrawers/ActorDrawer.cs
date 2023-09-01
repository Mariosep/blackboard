using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Actor))]
public class ActorDrawer : PropertyDrawer
{
    private SerializedProperty actorProperty;
    
    private SerializedProperty idProperty;
    private SerializedProperty categoryIdProperty;
    private SerializedProperty actorSOProperty;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Get properties
        actorProperty = property;
        idProperty = property.FindPropertyRelative("id");
        categoryIdProperty = property.FindPropertyRelative("categoryId");
        actorSOProperty = property.FindPropertyRelative("actorData");

        var actorSo = (ActorSO) actorSOProperty.objectReferenceValue;
        
        var actorDropdown = new ActorDropdown();
        actorDropdown.SetName(property.name.Capitalize());
        
        if(actorSo != null)
            actorDropdown.SetActor(actorSo);

        actorDropdown.onActorSelected += SetActor;

        actorDropdown.style.marginTop = 5;
        actorDropdown.style.marginBottom = 10;
        
        return actorDropdown;
    }

    private void SetActor(ActorSO actorSelected)
    {
        idProperty.stringValue = actorSelected.id;
        categoryIdProperty.stringValue = actorSelected.groupId;
        
        actorSOProperty.objectReferenceValue = actorSelected;
        
        actorProperty.serializedObject.ApplyModifiedProperties();
    }
}

public static class StringExtension
{
    public static string Capitalize(this string s)
    {
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
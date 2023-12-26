using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ActorSO))]
public class ActorDrawer : PropertyDrawer
{
    private SerializedProperty actorProperty;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        actorProperty = property;

        var actorDropdown = new ActorDropdown();
        actorDropdown.SetName(property.name.Capitalize());
        
        if(actorProperty.objectReferenceValue != null)
            actorDropdown.SetActor((ActorSO)actorProperty.objectReferenceValue);

        actorDropdown.onActorSelected += SetActor;
        
        actorDropdown.style.marginTop = 5;
        actorDropdown.style.marginBottom = 10;

        return actorDropdown;
    }
    
    private void SetActor(ActorSO actorSelected)
    {
        actorProperty.objectReferenceValue = actorSelected;
        actorProperty.serializedObject.ApplyModifiedProperties();
    }
}
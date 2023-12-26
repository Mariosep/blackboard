using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(InteractionsSO))]
public class InteractionsSOEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        InteractionsSO interactions = (InteractionsSO) target;
        interactions.Init();

        return root;
    }
}

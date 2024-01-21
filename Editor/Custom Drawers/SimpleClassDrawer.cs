using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SimpleClass))]
public class SimpleClassDrawer : PropertyDrawer
{
    private SerializedProperty simpleTextProperty;
    private SerializedProperty simpleIntProperty;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Get properties
        
        simpleTextProperty = property.FindPropertyRelative("simpleText");
        simpleIntProperty = property.FindPropertyRelative("simpleInt");
        
        VisualElement root = new VisualElement();

        TextField simpleTextField = new TextField("Simple Text");
        simpleTextField.BindProperty(simpleTextProperty);
        
        IntegerField simpleIntegerField = new IntegerField("Simple Int");
        simpleIntegerField.BindProperty(simpleIntProperty);
        
        root.Add(simpleTextField);
        root.Add(simpleIntegerField);

        return root;
    }
}
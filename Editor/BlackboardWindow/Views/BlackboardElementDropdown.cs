using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blackboard;
using UnityEditor;
using UnityEngine.UIElements;

public class BlackboardElementDropdown : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BlackboardElementDropdown, UxmlTraits> { }
    
    private const string UxmlPath = "UXML/ElementDropdown.uxml";

    public Action<BlackboardElementSO> onElementSelected;

    public List<BlackboardElementType> elementTypesAllowed;

    public BlackboardElementSO elementSelected;
    
    private Label nameLabel;
    private Button buttonPopup;

    public BlackboardElementDropdown()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        elementTypesAllowed = new List<BlackboardElementType>()
        {
            BlackboardElementType.Fact,
            BlackboardElementType.Event,
            BlackboardElementType.Actor,
            BlackboardElementType.Item
        };
        
        //style.flexGrow = 1;
        style.flexShrink = 1;
        
        nameLabel = this.Q<Label>("name-label");
        
        buttonPopup = this.Q<Button>();
        buttonPopup.clicked += () => BlackboardElementSearchWindow.Open(SetElement, elementTypesAllowed);
        
        UpdateButtonText();
    }
    
    public void SetName(string elementName)
    {
        nameLabel.text = elementName;
        nameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }
    
    public void SetElementTypesAllowed(params BlackboardElementType[] elementTypes)
    {
        elementTypesAllowed = elementTypes.ToList();
    }
    
    public void SetElement(BlackboardElementSO newElementSelected)
    {
        if(elementSelected == newElementSelected)
            return;
        
        if(elementSelected != null)
            elementSelected.onNameChanged -= UpdateButtonText;
        
        elementSelected = newElementSelected;
        
        elementSelected.onNameChanged += UpdateButtonText;
        
        UpdateButtonText();
        
        onElementSelected?.Invoke(newElementSelected);
    }
    
    private void UpdateButtonText()
    {
        string elementPath = "";

        if (elementSelected != null)
            elementPath = BlackboardEditorManager.instance.GetElementPath(elementSelected);
        
        buttonPopup.text = elementPath;
    }
}
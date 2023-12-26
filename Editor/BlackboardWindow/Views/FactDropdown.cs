using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class FactDropdown : VisualElement
{
    private const string UxmlPath = "UXML/ElementDropdown.uxml";
    
    public Action<FactSO> onFactSelected;
    public Action<FactType> onFactTypeSelectedChanged;

    public FactSO factSelected;
    public FactType factType;

    private bool filterByFactType = false;
    
    private Label nameLabel;
    private Button buttonPopup;
    
    public FactDropdown()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        nameLabel = this.Q<Label>("name-label");
        
        buttonPopup = this.Q<Button>();
        buttonPopup.clicked += OpenSearchWindow;
        
        UpdateButtonText();
    }
    
    public void SetName(string factName)
    {
        nameLabel.text = factName;
        nameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }

    public void SetWidth(float width)
    {
        this.style.width = width;
    }

    public void SetFactType(FactType factType)
    {
        this.factType = factType;
        filterByFactType = true;
    }
    
    private void OpenSearchWindow()
    {
        var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        
        var factPairs = GetFactPairs();

        var factSearchProvider = ScriptableObject.CreateInstance<FactSearchProvider>();
        factSearchProvider.Init(factPairs, SetFact);
        
        SearchWindow.Open(new SearchWindowContext(mousePos), factSearchProvider);
    }
    
    private List<KeyValuePair<FactSO, string>> GetFactPairs()
    {
        if(filterByFactType)
            return BlackboardEditorManager.instance.FactDataBase.GetPairs(factType);
        else
            return BlackboardEditorManager.instance.FactDataBase.GetPairs();
    }
    
    public void BindFact(FactSO fact)
    {
        if(factSelected != null)
            factSelected.onNameChanged -= UpdateButtonText;
        
        factSelected = fact;
        
        factSelected.onNameChanged += UpdateButtonText;
    }
    
    public void SetFact(FactSO newFactSelected)
    {
        if(factSelected == newFactSelected)
            return;
        
        BindFact(newFactSelected);
        
        UpdateButtonText();
        
        onFactSelected?.Invoke(newFactSelected);
        
        if (factType != newFactSelected.type)
        {
            factType = newFactSelected.type;
            onFactTypeSelectedChanged?.Invoke(factType);
        }
    }
    
    private void UpdateButtonText()
    {
        string factPath = "";

        if (factSelected != null)
            factPath = BlackboardEditorManager.instance.GetFactPath(factSelected.groupId, factSelected.id);
        
        buttonPopup.text = factPath;
    }
}
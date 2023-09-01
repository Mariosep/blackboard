using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemDropdown : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemDropdown, UxmlTraits> { }
    
    private const string UxmlPath = "UXML/ElementDropdown.uxml";
    
    public Action<ItemSO> onItemSelected;
    
    public ItemSO itemSelected;
    
    private Label nameLabel;
    private Button buttonPopup;

    public ItemDropdown()
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
    
    public void SetName(string itemName)
    {
        nameLabel.text = itemName;
        nameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
    }
    
    public void FitSize()
    {
        buttonPopup.style.flexGrow = 0;
        buttonPopup.style.width = new StyleLength(StyleKeyword.Auto);
    }
    
    private void OpenSearchWindow()
    {
        var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        
        var itemPairs = GetItemPairs();

        var itemSearchProvider = ScriptableObject.CreateInstance<ItemSearchProvider>();
        itemSearchProvider.Init(itemPairs, SetItem);
        
        SearchWindow.Open(new SearchWindowContext(mousePos), itemSearchProvider);
    }
    
    private List<KeyValuePair<ItemSO, string>> GetItemPairs()
    {
        return BlackboardManager.instance.ItemDataBase.GetPairs();
    }
    
    public void BindItem(ItemSO item)
    {
        if(itemSelected != null)
            itemSelected.onNameChanged -= UpdateButtonText;
        
        itemSelected = item;

        itemSelected.onNameChanged += UpdateButtonText;
    }

    public void SetItem(ItemSO newItemSelected)
    {
        if(this.itemSelected == newItemSelected)
            return;
        
        BindItem(newItemSelected);
        
        UpdateButtonText();
        
        onItemSelected?.Invoke(newItemSelected);
    }
    
    private void UpdateButtonText()
    {
        string itemPath = "";

        if (itemSelected != null)
            itemPath = BlackboardManager.instance.GetItemPath(itemSelected.groupId, itemSelected.id);
        
        buttonPopup.text = itemPath;
    }
}
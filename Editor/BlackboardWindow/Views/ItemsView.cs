using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

public class ItemsView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemsView, UxmlTraits> { }
    
    private readonly string uxmlPath = "UXML/Items.uxml";
    
    private ItemGroupSO _itemGroup;
    
    private GroupHeaderView _groupHeader;
    private VisualElement _itemsContainer;
    private ItemsListView _itemsListView;
    private Button _addItemButton;
    private Button _removeItemButton;

    private List<ItemSO> _items => _itemGroup.elementsList;
    
    public ItemsView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        _groupHeader = this.Q<GroupHeaderView>();
        _itemsContainer = this.Q<VisualElement>("items__container");
        _addItemButton = this.Q<Button>("add-item__button");
        _removeItemButton = this.Q<Button>("remove-item__button");
        
        RegisterCallbacks();
    }
    
    private void RegisterCallbacks()
    {
        _addItemButton.clicked += OnAddItemButtonClicked;
        _removeItemButton.clicked += OnRemoveItemButtonClicked;
        
        EditorApplication.playModeStateChanged += UpdateView;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _addItemButton.clicked -= OnAddItemButtonClicked;
        _removeItemButton.clicked -= OnRemoveItemButtonClicked;
        
        EditorApplication.playModeStateChanged -= UpdateView;
    }
    
    public void PopulateView(ItemGroupSO itemGroup)
    {
        _itemGroup = itemGroup;
        
        _groupHeader.SetGroup(itemGroup, BlackboardElementType.Item);
        
        if(_itemsListView != null)
            _itemsContainer.Remove(_itemsListView); 
        
        _itemsListView = new ItemsListView();
        _itemsListView.Populate(_itemGroup);
        
        _itemsContainer.Add(_itemsListView);
    }
    
    private void UpdateView(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.ExitingPlayMode)
        {
            if(_itemGroup != null)
                PopulateView(_itemGroup);    
        }
    }
    
    private void AddItem()
    {
        ItemSO newItem = BlackboardElementFactory.CreateItem();
        ScriptableObjectUtility.SaveSubAsset(newItem, BlackboardEditorManager.instance.Blackboard);
        
        _itemsListView.Add(newItem);
        
        EditorUtility.SetDirty(_itemGroup);
        AssetDatabase.SaveAssets();
    }

    private void RemoveItem(params ItemSO[] items)
    {
        _itemsListView.Remove(items);
        ScriptableObjectUtility.DeleteSubAsset(items);
        
        EditorUtility.SetDirty(_itemGroup);
        AssetDatabase.SaveAssets();
    }
    
    private void OnAddItemButtonClicked()
    {
        AddItem();
    }
    
    private void OnRemoveItemButtonClicked()
    {
        ItemSO[] itemsSelected = _itemsListView.itemsSelected;
        
        if(itemsSelected.Length == 0 && _items.Count > 0)
            itemsSelected = new [] { _items.Last() };
        
        if(itemsSelected.Length > 0)
        {
            ShowConfirmItemDeletionPopUp(itemsSelected);
        }
    }

    private void ShowConfirmItemDeletionPopUp(params ItemSO[] items)
    {
        bool deleteClicked = EditorUtility.DisplayDialog(
            "Delete item selected?",
            "Are you sure you want to delete this item",
            "Delete", 
            "Cancel");

        if (deleteClicked)
        {
            RemoveItem(items);
        }
    }
}
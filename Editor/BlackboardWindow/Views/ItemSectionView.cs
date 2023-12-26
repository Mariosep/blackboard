using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class ItemSectionView : VisualElement
{
    private readonly string uxmlPath = "UXML/ItemSection.uxml";
    
    private ItemDataBaseSO _itemDataBase;
    private ItemGroupSO _itemGroupSelected;
    
    private GroupSelectorView _groupSelectorView;
    private ItemsView _itemsView;

    public ItemSectionView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        _groupSelectorView = this.Q<GroupSelectorView>();
        _itemsView = this.Q<ItemsView>();
        
        RegisterCallbacks();
    }

    public void PopulateView(ItemDataBaseSO itemDataBase)
    {
        _itemDataBase = itemDataBase;
        
        _groupSelectorView.PopulateView(_itemDataBase);

        if (_itemDataBase.groupsList.Count == 0)
            _itemsView.visible = false;
    }

    private void RegisterCallbacks()
    {
        _groupSelectorView.onGroupSelected += OnGroupSelected;
        _groupSelectorView.onGroupListChanged += OnGroupListChanged;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _groupSelectorView.onGroupSelected -= OnGroupSelected;
        _groupSelectorView.onGroupListChanged -= OnGroupListChanged;
    }

    private void OnGroupSelected(int groupIndex)
    {
        if(groupIndex != -1 && _itemDataBase.groupsList.Count > groupIndex)
            ShowGroup(_itemDataBase.groupsList[groupIndex]);
        else
            HideItemView();
    }

    private void OnGroupListChanged()
    {
        OnGroupSelected(_groupSelectorView.GroupIndexSelected);
    }
    
    private void ShowGroup(ItemGroupSO itemGroup)
    {
        if (_itemGroupSelected != null)
        {
            EditorUtility.SetDirty(_itemGroupSelected);
            AssetDatabase.SaveAssets();
        }
        
        if (itemGroup != null)
        {
            _itemGroupSelected = itemGroup;
            
            _itemsView.PopulateView(itemGroup);
            _itemsView.visible = true;
        }
    }

    private void HideItemView()
    {
        _itemsView.visible = false;
    }
}
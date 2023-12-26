using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class FactSectionView : VisualElement
{
    private readonly string uxmlPath = "UXML/FactSection.uxml";
    
    private FactDataBaseSO _factDataBase;
    private FactGroupSO _factGroupSelected;
    
    private GroupSelectorView _groupSelectorView;
    private FactsView _factsView;

    public FactSectionView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        _groupSelectorView = this.Q<GroupSelectorView>();
        _factsView = this.Q<FactsView>();
        
        RegisterCallbacks();
    }

    public void PopulateView(FactDataBaseSO factDataBase)
    {
        _factDataBase = factDataBase;
        
        _groupSelectorView.PopulateView(factDataBase);
        
        if (_factDataBase.groupsList.Count == 0)
            _factsView.visible = false;
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
        if(groupIndex != -1 && _factDataBase.groupsList.Count > groupIndex)
            ShowGroup(_factDataBase.groupsList[groupIndex]);
        else
            HideItemView();
    }
    
    private void OnGroupListChanged()
    {
        OnGroupSelected(_groupSelectorView.GroupIndexSelected);
    }
    
    private void ShowGroup(FactGroupSO factGroup)
    {
        if (_factGroupSelected != null)
        {
            EditorUtility.SetDirty(_factGroupSelected);
            AssetDatabase.SaveAssets();
        }
        
        if (factGroup != null)
        {
            _factGroupSelected = factGroup;
            
            _factsView.PopulateView(factGroup);
            _factsView.visible = true;
        }
    }
    
    private void HideItemView()
    {
        _factsView.visible = false;
    }
}
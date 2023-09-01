using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class ActorSectionView : VisualElement
{
    private readonly string uxmlPath = "UXML/ActorSection.uxml";
    
    private ActorDataBaseSO _actorDataBase;
    private ActorGroupSO _actorGroupSelected;
    
    private GroupSelectorView _groupSelectorView;
    private ActorsView _actorsView;

    public ActorSectionView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        style.flexGrow = 1;
        
        _groupSelectorView = this.Q<GroupSelectorView>();
        _actorsView = this.Q<ActorsView>();
        
        RegisterCallbacks();
    }

    public void PopulateView(ActorDataBaseSO actorDataBase)
    {
        _actorDataBase = actorDataBase;
        
        _groupSelectorView.PopulateView(_actorDataBase);
        
        if (_actorDataBase.groupsList.Count == 0)
            _actorsView.visible = false;
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
        if(groupIndex != -1 && _actorDataBase.groupsList.Count > groupIndex)
            ShowGroup(_actorDataBase.groupsList[groupIndex]);
        else
            HideItemView();
    }
    
    private void OnGroupListChanged()
    {
        OnGroupSelected(_groupSelectorView.GroupIndexSelected);
    }
    
    private void ShowGroup(ActorGroupSO actorGroup)
    {
        if (_actorGroupSelected != null)
        {
            EditorUtility.SetDirty(_actorGroupSelected);
            AssetDatabase.SaveAssets();
        }
        
        if (actorGroup != null)
        {
            _actorGroupSelected = actorGroup;
            
            _actorsView.PopulateView(actorGroup);
            _actorsView.visible = true;
        }
    }
    
    private void HideItemView()
    {
        _actorsView.visible = false;
    }
}
using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

public class GroupSelectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<GroupSelectorView, UxmlTraits> { }

    private const string UxmlPath = "UXML/GroupSelector.uxml";

    public Action<int> onGroupSelected;
    public Action onGroupListChanged;

    private IDataBase _dataBase;

    private VisualElement _groupsContainer;
    
    private Button _addGroupButton;
    private Button _removeGroupButton;

    private CreateGroupView _createGroupView;
    
    private GroupListView _groupListView;
    
    public int GroupIndexSelected { get; private set; }
    
    public GroupSelectorView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, UxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);
        
        // Get references
        _addGroupButton = this.Q<Button>("add-group__button");
        _removeGroupButton = this.Q<Button>("remove-group__button");
        _groupsContainer = this.Q<VisualElement>("groups-container");
        
        _groupListView = new GroupListView();
        _groupsContainer.Add(_groupListView);
        
        RegisterCallbacks();
    }

    private void RegisterCallbacks()
    {
        _addGroupButton.clicked += OnAddGroupButtonClicked;
        _removeGroupButton.clicked += OnRemoveGroupButtonClicked;
        
        _groupListView.onGroupSelected += OnGroupSelected; 
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _groupListView.onGroupSelected -= OnGroupSelected;
        
        _addGroupButton.clicked -= AddCreateGroupView;
    }

    public void PopulateView(IDataBase dataBase)
    {
        _dataBase = dataBase;
        
        _groupListView.PopulateView(dataBase);
    }
    
    private void OnRemoveGroupButtonClicked()
    {
        ShowConfirmGroupDeletionPopUp(GroupIndexSelected);
    }
    
    private void OnAddGroupButtonClicked()
    {
        AddCreateGroupView();
    }

    private void AddCreateGroupView()
    {
        if (_createGroupView == null)
        {
            _createGroupView = new CreateGroupView(_dataBase.Type);
            Add(_createGroupView);
            _createGroupView.SetFocus();

            _createGroupView.onConfirm += CreateGroup;
            _createGroupView.onCancel += RemoveCreateGroupView;
        }
    }

    private void RemoveCreateGroupView()
    {
        if (_createGroupView != null)
        {
            Remove(_createGroupView);

            _createGroupView.onConfirm -= CreateGroup;
            _createGroupView.onCancel -= RemoveCreateGroupView;
            _createGroupView = null;
        }
    }

    private void CreateGroup(string groupName)
    {
        RemoveCreateGroupView();
        
        AddGroup(groupName);
    }

    private void OnGroupSelected(int groupIndex)
    {
        GroupIndexSelected = groupIndex;
        
        onGroupSelected?.Invoke(groupIndex);
    }

    private void AddGroup(string groupName)
    {
        var newGroup = GroupFactory.CreateGroup(groupName, _dataBase.Type);
        ScriptableObjectUtility.SaveSubAsset(newGroup, BlackboardManager.instance.Blackboard);
        
        _groupListView.AddGroup(newGroup);

        _dataBase.Save();
        
        onGroupListChanged?.Invoke();
    }

    private void RemoveGroup(string id)
    {
        var group = _dataBase.GetGroupById(id);
        
        _groupListView.RemoveGroup(id);
        ScriptableObjectUtility.DeleteSubAsset(group);
        
        _dataBase.Save();
        
        onGroupListChanged?.Invoke();
    }

    private void ShowConfirmGroupDeletionPopUp(int groupIndex)
    {
        bool deleteClicked = EditorUtility.DisplayDialog(
            "Delete group selected?",
            "Are you sure you want to delete this group",
            "Delete",
            "Cancel");

        if (deleteClicked)
        {
            string groupId = _dataBase.GetIdByIndex(groupIndex); 
            
            RemoveGroup(groupId);
        }
    }
}
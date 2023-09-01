using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

public class ActorsView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ActorsView, UxmlTraits> { }
    
    private readonly string uxmlPath = "UXML/Actors.uxml";
    
    private ActorGroupSO _actorGroup;
    
    private GroupHeaderView _groupHeader;
    private VisualElement _actorsContainer;
    private ActorsListView _actorListView;
    private Button _addActorButton;
    private Button _removeActorButton;

    private List<ActorSO> _actors => _actorGroup.elementsList;
    
    public ActorsView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        _groupHeader = this.Q<GroupHeaderView>();
        _actorsContainer = this.Q<VisualElement>("actors__container");
        _addActorButton = this.Q<Button>("add-actor__button");
        _removeActorButton = this.Q<Button>("remove-actor__button");
        
        RegisterCallbacks();
    }
    
    private void RegisterCallbacks()
    {
        _addActorButton.clicked += OnAddActorButtonClicked;
        _removeActorButton.clicked += OnRemoveActorButtonClicked;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _addActorButton.clicked -= OnAddActorButtonClicked;
        _removeActorButton.clicked -= OnRemoveActorButtonClicked;
    }
    
    public void PopulateView(ActorGroupSO actorGroup)
    {
        _actorGroup = actorGroup;
        
        _groupHeader.SetGroup(actorGroup, BlackboardElementType.Actor);
        
        if(_actorListView != null)
            _actorsContainer.Remove(_actorListView); 
        
        _actorListView = new ActorsListView();
        _actorListView.Populate(_actorGroup);
        
        _actorsContainer.Add(_actorListView);
    }
    
    private void AddActor()
    {
        ActorSO newActor = BlackboardElementFactory.CreateActor();
        ScriptableObjectUtility.SaveSubAsset(newActor, BlackboardManager.instance.Blackboard);
        
        _actorListView.Add(newActor);
        
        EditorUtility.SetDirty(_actorGroup);
        AssetDatabase.SaveAssets();
    }

    private void RemoveActor(params ActorSO[] actors)
    {
        _actorListView.Remove(actors);
        ScriptableObjectUtility.DeleteSubAsset(actors);
        
        EditorUtility.SetDirty(_actorGroup);
        AssetDatabase.SaveAssets();
    }
    
    private void OnAddActorButtonClicked()
    {
        AddActor();
    }
    
    private void OnRemoveActorButtonClicked()
    {
        ActorSO[] actorsSelected = _actorListView.actorsSelected;
        
        if(actorsSelected.Length == 0 && _actors.Count > 0)
            actorsSelected = new [] { _actors.Last() };
        
        if(actorsSelected.Length > 0)
        {
            ShowConfirmActorDeletionPopUp(actorsSelected);
        }
    }

    private void ShowConfirmActorDeletionPopUp(params ActorSO[] actors)
    {
        bool deleteClicked = EditorUtility.DisplayDialog(
            "Delete actor selected?",
            "Are you sure you want to delete this actor",
            "Delete", 
            "Cancel");

        if (deleteClicked)
        {
            RemoveActor(actors);
        }
    }
}
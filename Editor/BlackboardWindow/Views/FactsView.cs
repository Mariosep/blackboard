using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

public class FactsView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<FactsView, UxmlTraits> { }
    
    private readonly string uxmlPath = "UXML/Facts.uxml";
    
    private FactGroupSO _factGroup;
    
    private GroupHeaderView _groupHeader;
    private VisualElement _factsContainer;
    private FactsListView _factsListView;
    private Button _addFactButton;
    private Button _removeFactButton;

    private List<FactSO> _facts => _factGroup.elementsList;
    
    public FactsView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        _groupHeader = this.Q<GroupHeaderView>();
        _factsContainer = this.Q<VisualElement>("facts__container");
        _addFactButton = this.Q<Button>("add-fact__button");
        _removeFactButton = this.Q<Button>("remove-fact__button");
        
        RegisterCallbacks();
    }
    
    private void RegisterCallbacks()
    {
        _addFactButton.clicked += OnAddFactButtonClicked;
        _removeFactButton.clicked += OnRemoveFactButtonClicked;
        FactPopUpMenu.onPopupMenuOptionSelected += AddFact;
        
        EditorApplication.playModeStateChanged += UpdateView;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }

    private void UnregisterCallbacks()
    {
        _addFactButton.clicked -= OnAddFactButtonClicked;
        _removeFactButton.clicked -= OnRemoveFactButtonClicked;
        FactPopUpMenu.onPopupMenuOptionSelected -= AddFact;
        
        EditorApplication.playModeStateChanged -= UpdateView;
        
        if(_factsListView != null)
            _factsListView.onFactTypeChanged += OnFactTypeChanged;
    }
    
    public void PopulateView(FactGroupSO factGroup)
    {
        _factGroup = factGroup;
        
        _groupHeader.SetGroup(factGroup, BlackboardElementType.Fact);
        
        if(_factsListView != null)
            _factsContainer.Remove(_factsListView); 
        
        _factsListView = new FactsListView();
        _factsListView.Populate(_factGroup);
        
        _factsListView.onFactTypeChanged += OnFactTypeChanged;
        
        _factsContainer.Add(_factsListView);
    }

    private void UpdateView(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.ExitingPlayMode)
        {
            if(_factGroup != null)
                PopulateView(_factGroup);    
        }
    }
    
    private void AddFact(FactType factType)
    {
        FactSO newFact = BlackboardElementFactory.CreateFact(factType);
        ScriptableObjectUtility.SaveSubAsset(newFact, BlackboardEditorManager.instance.Blackboard);
        
        _factsListView.Add(newFact);
        
        EditorUtility.SetDirty(_factGroup);
        AssetDatabase.SaveAssets();
    }

    private void RemoveFact(params FactSO[] facts)
    {
        _factsListView.Remove(facts);
        ScriptableObjectUtility.DeleteSubAsset(facts);
        
        EditorUtility.SetDirty(_factGroup);
        AssetDatabase.SaveAssets();
    }
    
    private void OnAddFactButtonClicked()
    {
        FactPopUpMenu.DisplayAddFactPopupMenu();
    }
    
    private void OnRemoveFactButtonClicked()
    {
        FactSO[] factsSelected = _factsListView.factsSelected;
        
        if(factsSelected.Length == 0 && _facts.Count > 0)
            factsSelected = new [] { _facts.Last() };
        
        if(factsSelected.Length > 0)
        {
            ShowConfirmFactDeletionPopUp(factsSelected);
        }
    }

    private void OnFactTypeChanged(int i, FactType newType)
    {
        FactSO factToReplace = _facts[i];

        if(newType == factToReplace.type)
            return;
        
        FactSO newFact = BlackboardElementFactory.CreateFact(newType, factToReplace.id);
        newFact.groupId = factToReplace.groupId;
        newFact.theName = factToReplace.theName;

        ScriptableObjectUtility.DeleteSubAsset(factToReplace);
        ScriptableObjectUtility.SaveSubAsset(newFact, BlackboardEditorManager.instance.Blackboard);

        _factsListView.Replace(i, newFact);
        
        EditorUtility.SetDirty(_factGroup);
        AssetDatabase.SaveAssets();
    }
    
    private void ShowConfirmFactDeletionPopUp(params FactSO[] facts)
    {
        bool deleteClicked = EditorUtility.DisplayDialog(
            "Delete fact selected?",
            "Are you sure you want to delete this fact",
            "Delete", 
            "Cancel");

        if (deleteClicked)
        {
            RemoveFact(facts);
        }
    }
}
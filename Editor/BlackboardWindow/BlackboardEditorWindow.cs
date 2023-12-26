using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackboardEditorWindow : EditorWindow
{
    public static string RelativePath => AssetDataBaseExtensions.GetDirectoryOfScript<BlackboardEditorWindow>();
    
    private readonly string uxmlPath = "UXML/BlackboardEditor.uxml";
    
    private BlackboardSO _blackboard;
    
    private Button factsTabButton;
    private Button eventsTabButton;
    private Button actorsTabButton;
    private Button itemsTabButton;
    
    private VisualElement blackboardContainer;
    
    public enum BlackboardTab
    {
        None,
        Facts,
        Events,
        Actors,
        Items
    }
    
    public BlackboardTab blackboardTabSelected;

    private CreateGroupView _createGroupView;

    public BlackboardSO Blackboard => _blackboard;
    
    [MenuItem("Tools/Blackboard")]
    public static void OpenWindow()
    {
        BlackboardEditorWindow wnd = GetWindow<BlackboardEditorWindow>("Blackboard");
        wnd.minSize = new Vector2(650, 300);
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject is not BlackboardSO) 
            return false;
        OpenWindow();
        return true;
    }
    
    public void CreateGUI()
    {
        string path = Path.Combine(RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(rootVisualElement);

        factsTabButton = rootVisualElement.Q<Button>("facts__tab-button");
        eventsTabButton = rootVisualElement.Q<Button>("events__tab-button");
        actorsTabButton = rootVisualElement.Q<Button>("actors__tab-button");
        itemsTabButton = rootVisualElement.Q<Button>("items__tab-button");
        
        blackboardContainer = rootVisualElement.Q<VisualElement>("blackboard-content");
        
        _blackboard = BlackboardEditorManager.instance.Blackboard;
        
        if(_blackboard == null)
            ShowCreateBlackboardPopUp();
        else
            ShowFactsTab();
        
        RegisterCallbacks();
    }

    private void RegisterCallbacks()
    {
        factsTabButton.clicked += OnFactsTabButtonClicked;
        eventsTabButton.clicked += OnEventsTabButtonClicked;
        actorsTabButton.clicked += OnActorsTabButtonClicked;
        itemsTabButton.clicked += OnItemsTabButtonClicked;
    }

    private void UnregisterCallbacks()
    {
        factsTabButton.clicked -= OnFactsTabButtonClicked;
        eventsTabButton.clicked -= OnEventsTabButtonClicked;
        actorsTabButton.clicked -= OnActorsTabButtonClicked;
        itemsTabButton.clicked -= OnItemsTabButtonClicked;
    }

    private void OnFactsTabButtonClicked()
    {
        if (blackboardTabSelected != BlackboardTab.Facts)
            ShowFactsTab();
    }

    private void OnEventsTabButtonClicked()
    {
        if (blackboardTabSelected != BlackboardTab.Events)
            ShowEventsTab();
    }
    
    private void OnActorsTabButtonClicked()
    {
        if (blackboardTabSelected != BlackboardTab.Actors)
            ShowActorsTab();
    }
    
    private void OnItemsTabButtonClicked()
    {
        if (blackboardTabSelected != BlackboardTab.Items)
            ShowItemsTab();
    }

    private void ShowCreateBlackboardPopUp()
    {
        var _createBlackboardPopUpView = new CreateBlackboardPopUpView();
        rootVisualElement.Add(_createBlackboardPopUpView);

        _createBlackboardPopUpView.onConfirm += CreateBlackboard;
    }

    private void CreateBlackboard(string blackboardName)
    {
        BlackboardSO newBlackboard = BlackboardFactory.CreateBlackboard(blackboardName);
        
        

        BlackboardEditorManager.instance.Blackboard = newBlackboard;
        _blackboard = newBlackboard;
        
        ShowFactsTab();
    }

    private void ShowFactsTab()
    {
        eventsTabButton.RemoveFromClassList("tab-button--selected");
        actorsTabButton.RemoveFromClassList("tab-button--selected");
        itemsTabButton.RemoveFromClassList("tab-button--selected");
        
        factsTabButton.AddToClassList("tab-button--selected");
        
        blackboardTabSelected = BlackboardTab.Facts;

        var factSectionView = new FactSectionView();
        factSectionView.PopulateView(_blackboard.factDataBase);
        
        blackboardContainer.Clear();
        blackboardContainer.Add(factSectionView);
    }
    
    private void ShowEventsTab()
    {
        factsTabButton.RemoveFromClassList("tab-button--selected");
        actorsTabButton.RemoveFromClassList("tab-button--selected");
        itemsTabButton.RemoveFromClassList("tab-button--selected");
        
        eventsTabButton.AddToClassList("tab-button--selected");
        
        blackboardTabSelected = BlackboardTab.Events;
     
        var eventSectionView = new EventSectionView();
        eventSectionView.PopulateView(_blackboard.eventDataBase);
        
        blackboardContainer.Clear();
        blackboardContainer.Add(eventSectionView);
    }
    
    private void ShowActorsTab()
    {
        factsTabButton.RemoveFromClassList("tab-button--selected");
        eventsTabButton.RemoveFromClassList("tab-button--selected");
        itemsTabButton.RemoveFromClassList("tab-button--selected");
        
        actorsTabButton.AddToClassList("tab-button--selected");
        
        blackboardTabSelected = BlackboardTab.Actors;
     
        var actorSectionView = new ActorSectionView();
        actorSectionView.PopulateView(_blackboard.actorDataBase);
        
        blackboardContainer.Clear();
        blackboardContainer.Add(actorSectionView);
    }

    private void ShowItemsTab()
    {
        factsTabButton.RemoveFromClassList("tab-button--selected");
        eventsTabButton.RemoveFromClassList("tab-button--selected");
        actorsTabButton.RemoveFromClassList("tab-button--selected");
        
        itemsTabButton.AddToClassList("tab-button--selected");
        
        blackboardTabSelected = BlackboardTab.Items;
     
        var itemSectionView = new ItemSectionView();
        itemSectionView.PopulateView(_blackboard.itemDataBase);
        
        blackboardContainer.Clear();
        blackboardContainer.Add(itemSectionView);
    }
    
    private void OnDestroy()
    {
        UnregisterCallbacks();    
        
        if(_blackboard != null)
        {
            EditorUtility.SetDirty(_blackboard);
            AssetDatabase.SaveAssets();
        }
    }
}
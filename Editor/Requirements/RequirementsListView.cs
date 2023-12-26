using System.Collections.Generic;
using System.IO;
using Blackboard;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RequirementsListView : VisualElement
{
    public new class UxmlFactory: UxmlFactory<RequirementsListView, RequirementsListUxmlTraits> { }
    
    private readonly string uxmlName = "UXML/RequirementsListView.uxml";

    // Visual elements
    private Foldout headerFoldout;
    protected ListView listView;

    // State
    private bool foldoutExpanded;
    
    private RequirementsSO requirements;
    protected List<ConditionSO> conditions => requirements.conditions;
    
    // Configuration
    private bool saveEnabled = false;
    private ScriptableObject mainAsset;
    
    // Properties
    public string headerTitle { get; set; }
    
    public RequirementsListView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);
        
        headerFoldout = this.Q<Foldout>("requirement-list-header__foldout");
        listView = this.Q<ListView>();
        
        Setup();
        RegisterCallbacks();
    }
    
    private void Setup()
    {
        foldoutExpanded = true;
        
        var toggle = headerFoldout.Q<Toggle>();
        toggle.style.marginLeft = new StyleLength(3);
        
        listView.makeItem = MakeItem;
        listView.bindItem = (element, i) => BindItem(element, i);
    }
 
    public void Populate(RequirementsSO requirements)
    {
        this.requirements = requirements;
        listView.itemsSource = conditions;
        listView.bindingPath = "conditions";
    }
    
    public void SaveAsSubAssetOf(ScriptableObject mainAsset)
    {
        saveEnabled = true;
        this.mainAsset = mainAsset;
    }
    
    public void SetHeaderTitle(string newTitle)
    {
        newTitle ??= "Requirements";
        
        headerTitle = newTitle;
        headerFoldout.text = newTitle;
    }
    
    private void RegisterCallbacks()
    {
        RegisterCallback<AttachToPanelEvent>(e => SetHeaderTitle(headerTitle));
        
        headerFoldout.RegisterValueChangedCallback(evt =>
        {
            if (evt.target == headerFoldout)
            {
                ToggleFoldout();
            }
        });
        
        listView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(() =>
        {
            BlackboardElementSearchWindow.Open(AddCondition, new()
                {
                    BlackboardElementType.Fact,
                    BlackboardElementType.Event
                    
                });
        });
        
        listView.Q<Button>("unity-list-view__remove-button").clickable = new Clickable(() =>
        {
            if(listView.selectedIndex != -1)
                RemoveCondition(conditions[listView.selectedIndex]);
        });
    }
    
    private void ToggleFoldout()
    {
        foldoutExpanded = !foldoutExpanded;
            
        if (foldoutExpanded)
        {
            listView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }
        else
        {
            listView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
    }
    
    protected virtual void AddCondition(BlackboardElementSO elementSelected)
    {
        var newCondition = ConditionSOFactory.CreateCondition(elementSelected);

        if (saveEnabled)
        {
            AssetDatabase.AddObjectToAsset(newCondition, mainAsset);
            AssetDatabase.SaveAssets();
        }
        
        conditions.Add(newCondition);
        
        listView.RefreshItems();
    }

    protected virtual void RemoveCondition(ConditionSO condition)
    {
        conditions.Remove(condition);

        if (saveEnabled)
        {
            AssetDatabase.RemoveObjectFromAsset(condition);
            AssetDatabase.SaveAssets();
        }
        
        listView.RefreshItems();
    }

    private VisualElement MakeItem() => new ConditionView();

    private void BindItem(VisualElement element, int i)
    {
        var conditionView = element as ConditionView;
        
        conditionView.BindCondition(conditions[i]);
        conditionView.onConditionTypeChanged = (newCondition => OnConditionTypeChanged(newCondition, i));
    }

    protected virtual void OnConditionTypeChanged(ConditionSO newCondition, int i)
    {
        if (saveEnabled)
        {
            ScriptableObjectUtility.DeleteSubAsset(conditions[i]);
            ScriptableObjectUtility.SaveSubAsset(newCondition, mainAsset);
        }
        
        conditions[i] = newCondition;
        listView.RefreshItems();       
    }
    
    public class RequirementsListUxmlTraits : VisualElement.UxmlTraits
    {
        private UxmlStringAttributeDescription headerTitle = new() { name = "header-title", defaultValue = "Requirements"}; 
        
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var element = ve as RequirementsListView;

            element.headerTitle = headerTitle.GetValueFromBag(bag, cc);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ActorDropdown : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ActorDropdown, UxmlTraits> { }
    
    private const string UxmlPath = "UXML/ElementDropdown.uxml";

    public Action<ActorSO> onActorSelected;

    public ActorSO actorSelected;

    private Label nameLabel;
    private Button buttonPopup;

    public ActorDropdown()
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

    public void SetName(string actorName)
    {
        nameLabel.text = actorName;
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
        
        var actorPairs = GetActorPairs();

        var actorSearchProvider = ScriptableObject.CreateInstance<ActorSearchProvider>();
        actorSearchProvider.Init(actorPairs, SetActor);
        
        SearchWindow.Open(new SearchWindowContext(mousePos), actorSearchProvider);
    }
    
    private List<KeyValuePair<ActorSO, string>> GetActorPairs()
    {
        return BlackboardEditorManager.instance.ActorDataBase.GetPairs();
    }
    
    public void BindActor(ActorSO actor)
    {
        if(actorSelected != null)
            actorSelected.onNameChanged -= UpdateButtonText;
        
        actorSelected = actor;

        actorSelected.onNameChanged += UpdateButtonText;
    }

    public void SetActor(ActorSO newActorSelected)
    {
        if(this.actorSelected == newActorSelected)
            return;
        
        BindActor(newActorSelected);

        UpdateButtonText();
        
        onActorSelected?.Invoke(newActorSelected);
    }
    
    private void UpdateButtonText()
    {
        string actorPath = "";

        if (actorSelected != null)
            actorPath = BlackboardEditorManager.instance.GetActorPath(actorSelected.groupId, actorSelected.id);
        
        buttonPopup.text = actorPath;
    }
}
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateBlackboardPopUpView : VisualElement
{
    private readonly string uxmlPath = "UXML/CreateBlackboardPopUp.uxml";

    public Action<string> onConfirm;
    public System.Action onCancel; 
    
    private TextField nameField;
    private VisualElement nameInputText;
    private Label nameValidationLabel;
    private Button confirmButton;
    private Button cancelButton;
    
    private Func<string, bool> _groupExists;
    
    public CreateBlackboardPopUpView()
    {
        string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
        VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(this);

        //_groupExists = groupExists;
        
        style.position = new StyleEnum<Position>(Position.Absolute);
        style.top = 0;
        style.bottom = 0;
        style.left = 0;
        style.right = 0;
        
        // Get references
        nameField = this.Q<TextField>("name__field");
        nameInputText = nameField.ElementAt(1);
        nameValidationLabel = this.Q<Label>("name-validation__label");
        confirmButton = this.Q<Button>("confirm__button");
        cancelButton = this.Q<Button>("cancel__button");
        
        RegisterCallbacks();

        nameValidationLabel.text = "";
        
        nameField.Focus();
    }
    
    private void RegisterCallbacks()
    {
        confirmButton.clicked += OnConfirmClicked;
        cancelButton.clicked += OnCancelClicked;
        
        RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
    }
    
    private void UnregisterCallbacks()
    {
        confirmButton.clicked -= OnConfirmClicked;
        cancelButton.clicked -= OnCancelClicked;
    }

    private void OnConfirmClicked()
    {
        if (BlackboardValidator.ValidateBlackboardName(nameField.value, nameInputText, nameValidationLabel)) 
        {
            Debug.Log("Created");    
            onConfirm?.Invoke(nameField.value);

            RemoveFromHierarchy();
        }
    }

    private void OnCancelClicked()
    {
        onCancel?.Invoke();
        
        RemoveFromHierarchy();
    }
}
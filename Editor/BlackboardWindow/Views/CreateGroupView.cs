using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class CreateGroupView : VisualElement
    {
        private readonly string uxmlPath = "UXML/CreateGroup.uxml";

        public Action<string> onConfirm;
        public System.Action onCancel; 
    
        private TextField categoryField;
        private VisualElement categoryInputText;
        private Label categoryValidationLabel;
        private Button confirmButton;
        private Button cancelButton;

        private BlackboardElementType _blackboardElementType;
    
        public CreateGroupView(BlackboardElementType blackboardElementType)
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            _blackboardElementType = blackboardElementType;
        
            // Get references
            categoryField = this.Q<TextField>("category__field");
            categoryInputText = categoryField.ElementAt(1);
            categoryValidationLabel = this.Q<Label>("category-validation__label");
            confirmButton = this.Q<Button>("confirm__button");
            cancelButton = this.Q<Button>("cancel__button");
        
            RegisterCallbacks();

            categoryValidationLabel.text = "";
        }

        public void SetFocus()
        {
            categoryField.Focus();
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
            if (GroupValidator.Validate(categoryField.value, categoryInputText, categoryValidationLabel, _blackboardElementType))
                onConfirm?.Invoke(categoryField.value);
        }

        private void OnCancelClicked()
        {
            onCancel?.Invoke();
        }
    }
}
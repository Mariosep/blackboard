using System.IO;
using Blackboard.Actors;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class GroupHeaderView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<GroupHeaderView, UxmlTraits> { }
    
        private readonly string uxmlPath = "UXML/GroupHeader.uxml";

        private ScriptableObject _groupData;
        private SerializedProperty _groupNameProperty;
        private BlackboardElementType _blackboardElementType;
    
        // Visual elements
        private VisualElement _groupHeader;
        private Label _groupHeaderLabel;
        private TextField _groupNameField;
        private Label _groupValidationLabel;
        private Button _groupEditButton;
        private Button _groupEditConfirmButton;
        private Button _groupEditCancelButton;
    
        public GroupHeaderView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            this.style.flexShrink = 0;
        
            // Get references
            _groupHeader = this.Q<VisualElement>("group-header");
            _groupHeaderLabel = this.Q<Label>("group-header__label");
            _groupNameField = this.Q<TextField>("group-header__field");
            _groupValidationLabel = this.Q<Label>("group-validation__label");
            _groupEditButton = this.Q<Button>("group-edit__button");
            _groupEditConfirmButton = this.Q<Button>("group-edit__confirm__button");
            _groupEditCancelButton = this.Q<Button>("group-edit__cancel__button");
        
            Setup();
        
            RegisterCallbacks();
        }

        private void Setup()
        {
            Texture2D editIcon = EditorGUIUtility.IconContent("d_editicon.sml").image as Texture2D;
            Texture2D cancelIcon = EditorGUIUtility.IconContent("CrossIcon").image as Texture2D;
        
            _groupEditButton.ElementAt(0).style.backgroundImage = new StyleBackground(editIcon);
            _groupEditCancelButton.ElementAt(0).style.backgroundImage = new StyleBackground(cancelIcon);
        
            _groupEditConfirmButton.style.display = DisplayStyle.None;
            _groupEditCancelButton.style.display = DisplayStyle.None;
        }

        public void SetGroup(ScriptableObject groupData, BlackboardElementType blackboardElementType)
        {
            _groupData = groupData;
            _blackboardElementType = blackboardElementType;
        
            var so = new SerializedObject(groupData);
            _groupNameProperty = so.FindProperty("groupName");
            _groupHeaderLabel.BindProperty(_groupNameProperty);
        
            ShowGroupLabel();
        }
    
        private void RegisterCallbacks()
        {
            _groupEditButton.clicked += OnEditButtonClicked;
            _groupEditConfirmButton.clicked += OnConfirmEditButtonClicked;
            _groupEditCancelButton.clicked += OnCancelEditButtonClicked;
        
            RegisterCallback<DetachFromPanelEvent>(_ => UnregisterCallbacks());
        }

        private void UnregisterCallbacks()
        {
            _groupEditButton.clicked -= OnEditButtonClicked;
            _groupEditConfirmButton.clicked -= OnConfirmEditButtonClicked;
            _groupEditCancelButton.clicked -= OnCancelEditButtonClicked;
        }
    
        private void OnEditButtonClicked()
        {
            ShowGroupField();
        
            _groupNameField.Focus();
        }

        private void OnConfirmEditButtonClicked()
        {
            string newGroupName = _groupNameField.value;
        
            newGroupName = newGroupName.Replace(" ", "");
        
            if(GroupValidator.Validate(newGroupName, ElementAt(0), _groupValidationLabel, _blackboardElementType))
            {
                _groupNameProperty.stringValue = newGroupName;

                switch (_blackboardElementType)
                {
                    case BlackboardElementType.Actor:
                        var actorGroup = (ActorGroupSO)_groupData;
                        actorGroup.SetName(newGroupName);
                        break;
                
                    /*case BlackboardElementType.Event:
                        var eventGroup = (EventGroupSO)_groupData;
                        eventGroup.SetName(newGroupName);
                        break;*/
                
                    case BlackboardElementType.Fact:
                        var factGroup = (FactGroupSO)_groupData;
                        factGroup.SetName(newGroupName);
                        break;
                
                    case BlackboardElementType.Item:
                        var itemGroup = (ItemGroupSO)_groupData;
                        itemGroup.SetName(newGroupName);
                        break;
                }
            
                EditorUtility.SetDirty(_groupData);
                AssetDatabase.SaveAssets();
            
                ShowGroupLabel();
            }
            else
            {
                _groupNameField.Focus();
            }
        }
    
        private void OnCancelEditButtonClicked()
        {
            ShowGroupLabel();    
        }
    
        private void ShowGroupLabel()
        {
            _groupEditConfirmButton.style.display = DisplayStyle.None;
            _groupEditCancelButton.style.display = DisplayStyle.None;
            _groupNameField.style.display = DisplayStyle.None;
            _groupEditButton.style.display = DisplayStyle.Flex;
        
            _groupValidationLabel.text = "";
        
            _groupHeader.Insert(0, _groupHeaderLabel);
        }
    
        private void ShowGroupField()
        {
            _groupEditConfirmButton.style.display = DisplayStyle.Flex;
            _groupEditCancelButton.style.display = DisplayStyle.Flex;
            _groupEditButton.style.display = DisplayStyle.None;
        
            _groupHeader.Remove(_groupHeaderLabel);

            _groupNameField.value = _groupNameProperty.stringValue;
            _groupNameField.style.display = DisplayStyle.Flex;
        }
    }
}
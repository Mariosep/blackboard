using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blackboard.Actors;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Actors
{
    public class ActorsListView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<ActorsListView, UxmlTraits> { }
        
        private readonly string uxmlPath = "UXML/ActorsListView.uxml";

        private MultiColumnListView _listView;
        private ActorGroupSO _actorGroup;
        private SerializedProperty _eventsProperty;
        private List<ActorSO> _actors => _actorGroup.elementsList;

        public ActorSO[] actorsSelected => _listView.selectedItems.Cast<ActorSO>().ToArray();
    
        public ActorsListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
        
            _listView = this.Q<MultiColumnListView>();
        
            Setup();
        }

        private void Setup()
        {
            _listView.columns["name"].makeCell = MakeNameCell;
            _listView.columns["description"].makeCell = MakeDescriptionCell;
            _listView.columns["prefab"].makeCell = MakePrefabCell;
        
            _listView.columns["name"].bindCell = (element, i) => BindName(element, _actors[i]);
            _listView.columns["description"].bindCell = (element, i) => BindDescription(element, _actors[i]);
            _listView.columns["prefab"].bindCell = (element, i) => BindPrefab(element, new SerializedObject(_actors[i]));
        }
    
        public void Populate(ActorGroupSO actorGroup)
        {
            _actorGroup = actorGroup;
            _listView.itemsSource = _actors;
            _listView.RefreshItems();
        }

        #region Modify list
        public void Add(ActorSO actor)
        {
            string validName = BlackboardValidator.GetValidName(_actorGroup, "", actor.Name, actor, true);
            actor.Name = validName;
        
            _actorGroup.AddElement(actor);
            _listView.RefreshItems();

            int lastIndex = _actorGroup.elementsList.Count - 1;
        
            _listView.SetSelection(lastIndex);
            var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
            var itemView = list.ElementAt(lastIndex);
            var nameField = itemView.Q<TextField>("theName");
            nameField.Focus();
        }
    
        public void Remove(params ActorSO[] actors)
        {
            foreach (ActorSO actor in actors)
                _actorGroup.RemoveElement(actor.id);    
        
            _listView.RefreshItems();
        }
        #endregion

        #region Make
        private VisualElement MakeNameCell() => new TextFieldItem(nameof(ActorSO.theName));
        private VisualElement MakeDescriptionCell() => new TextFieldItem(nameof(ActorSO.description));
        private VisualElement MakePrefabCell()
        {
            var prefabField = new ObjectField();
            prefabField.objectType = typeof(GameObject);
            prefabField.bindingPath = "prefab";
            return prefabField;
        }
        #endregion
    
        #region Bind
        private void BindName(VisualElement cell, ActorSO actor)
        {
            var nameField = cell as TextFieldItem;
            nameField.SetDataSource(actor);
            nameField.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetName(e.previousValue, e.newValue, actor));
        }
        
        private void BindDescription(VisualElement cell, ActorSO actor)
        {
            var descriptionField = cell as TextFieldItem;
            descriptionField.SetDataSource(actor);
            descriptionField.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetDescription(e.previousValue, e.newValue, actor));
        }
    
        private void BindPrefab(VisualElement cell, SerializedObject serializedObject)
        {
            ObjectField prefabField = cell.Q<ObjectField>();
            prefabField.Bind(serializedObject);
        }
        #endregion
    }
}
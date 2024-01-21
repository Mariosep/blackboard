using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blackboard.Facts;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public class FactsListView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<FactsListView, UxmlTraits>{}
        
        private readonly string uxmlPath = "UXML/FactsListView.uxml";

        public Action<int, FactType> onFactTypeChanged;

        private MultiColumnListView _listView;
        private FactGroupSO _factGroup;
        private SerializedProperty _factsProperty;
        private List<FactSO> _facts => _factGroup.elementsList;

        public FactSO[] factsSelected => _listView.selectedItems.Cast<FactSO>().ToArray();

        public FactsListView()
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
            _listView.columns["value"].makeCell = MakeCell;
            _listView.columns["description"].makeCell = MakeDescriptionCell;
            _listView.columns["type"].makeCell = MakeTypeCell;

            _listView.columns["name"].bindCell = (element, i) => BindName(element, _facts[i]);
            _listView.columns["value"].bindCell = (element, i) => BindValue(element, _facts[i]);
            _listView.columns["description"].bindCell = (element, i) => BindDescription(element, _facts[i]);
            _listView.columns["type"].bindCell = BindType;
        }

        public void Populate(FactGroupSO factGroup)
        {
            _factGroup = factGroup;
            _listView.itemsSource = _facts;
            _listView.RefreshItems();
        }

        #region Modify list

        public void Add(FactSO fact)
        {
            string validName = BlackboardValidator.GetValidName(_factGroup, "",fact.Name, fact, true);
            fact.Name = validName;

            _factGroup.AddElement(fact);
            _listView.RefreshItems();

            int lastIndex = _factGroup.elementsList.Count - 1;

            _listView.SetSelection(lastIndex);
            var list = _listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
            var itemView = list.ElementAt(lastIndex);
            var nameField = itemView.Q<TextField>("theName");
            nameField.Focus();
        }

        public void Remove(params FactSO[] facts)
        {
            foreach (FactSO fact in facts)
                _factGroup.RemoveElement(fact.id);

            _listView.RefreshItems();
        }

        public void Replace(int i, FactSO fact)
        {
            _factGroup.Replace(i, fact);
            _listView.RefreshItem(i);
        }

        #endregion

        #region Make

        private VisualElement MakeCell() => new();
        private VisualElement MakeNameCell() => new TextFieldItem(nameof(FactSO.theName));
        private VisualElement MakeDescriptionCell() => new TextFieldItem(nameof(FactSO.description));
        private VisualElement MakeTypeCell() => new EnumFieldItem(nameof(FactSO.type), FactType.Bool);

        #endregion

        #region Bind

        private void BindName(VisualElement cell, FactSO fact)
        {
            var nameField = cell as TextFieldItem;
            nameField.SetDataSource(fact);
            nameField.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetName(e.previousValue, e.newValue, fact));
        }

        private void BindValue(VisualElement cell, FactSO fact)
        {
            if (cell.childCount > 0)
                cell.RemoveAt(0);

            VisualElement content = FactValueViewFactory.CreateValueView(fact);
            cell.Add(content);
        }

        private void BindDescription(VisualElement cell, FactSO fact)
        {
            var descriptionField = cell as TextFieldItem;
            descriptionField.SetDataSource(fact);
            descriptionField.textField.RegisterValueChangedCallback(e =>
                BlackboardValidator.ValidateAndSetDescription(e.previousValue, e.newValue, fact));
        }

        private void BindType(VisualElement cell, int i)
        {
            EnumField typeField = cell.Q<EnumField>();

            typeField.value = _facts[i].type;
            typeField.RegisterValueChangedCallback(e => onFactTypeChanged?.Invoke(i, (FactType)e.newValue));
        }

        #endregion
    }
}
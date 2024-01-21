using System;
using System.Collections.Generic;
using System.IO;
using Blackboard.Events;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventsListView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventsListView, UxmlTraits> {}

        private readonly string uxmlPath = "UXML/EventsListView.uxml";

        public Action<EventInfo> onEventSelected;

        // Data
        private EventChannelInfo _eventChannelInfo;
        private List<EventInfo> _events => _eventChannelInfo.eventList;

        // Visual elements
        private MultiColumnListView _listView;

        public EventsListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            _listView = this.Q<MultiColumnListView>();
            _listView.selectionChanged += OnEventSelected;

            Setup();
        }

        private void Setup()
        {
            _listView.columns["name"].makeCell = MakeNameCell;
            _listView.columns["parameters"].makeCell = MakeParametersCell;
            _listView.columns["description"].makeCell = MakeDescriptionCell;

            _listView.columns["name"].bindCell = (element, i) => BindName(element, _events[i]);
            _listView.columns["parameters"].bindCell = (element, i) => BindParameters(element, _events[i]);
            _listView.columns["description"].bindCell = (element, i) => BindDescription(element, _events[i]);
        }

        public void PopulateView(EventChannelInfo eventChannelInfo)
        {
            _eventChannelInfo = eventChannelInfo;
            _listView.itemsSource = _events;
            _listView.RefreshItems();

            //SetSelection(0);
        }

        #region Make

        private VisualElement MakeCell()
        {
            var cell = new VisualElement();
            cell.AddToClassList("centered-vertical");
            cell.style.paddingTop = 5f;
            cell.style.paddingBottom = 5f;

            return cell;
        }

        private VisualElement MakeNameCell()
        {
            var cell = MakeCell();

            var nameField = new Label("");
            nameField.name = "name-field";
            cell.Add(nameField);

            return cell;
        }

        private VisualElement MakeParametersCell()
        {
            var cell = MakeCell();

            var parametersField = new Label("");
            parametersField.name = "parameters-field";
            parametersField.enableRichText = true;
            cell.Add(parametersField);

            return cell;
        }

        private VisualElement MakeDescriptionCell()
        {
            var cell = MakeCell();

            var descriptionField = new Label("");
            descriptionField.name = "description-field";
            cell.Add(descriptionField);

            return cell;
        }

        #endregion

        #region Bind

        private void BindName(VisualElement cell, EventInfo eventInfo)
        {
            Label nameLabel = cell.Q<Label>();
            nameLabel.text = eventInfo.eventName;
        }

        private void BindParameters(VisualElement cell, EventInfo eventInfo)
        {
            Label parametersLabel = cell.Q<Label>();

            if (eventInfo.parameters.Count > 0)
            {
                ParameterInfo firstParameter = eventInfo.parameters[0];
                parametersLabel.text =
                    $"<color=#3698D5>{firstParameter.type.GetName()}</color> {firstParameter.parameterName}";
            }

            for (var index = 1; index < eventInfo.parameters.Count; index++)
            {
                ParameterInfo parameter = eventInfo.parameters[index];
                parametersLabel.text += $", <color=#3698D5>{parameter.type.GetName()}</color> {parameter.parameterName}";
            }
        }

        private void BindDescription(VisualElement cell, EventInfo eventInfo)
        {
            Label descriptionField = cell.Q<Label>();
            descriptionField.text = eventInfo.description;
        }

        #endregion

        public void SetSelection(int index)
        {
            if (_listView.itemsSource != null && _listView.itemsSource.Count > index)
                _listView.SetSelection(index);
        }

        private void OnEventSelected(IEnumerable<object> itemsSelected)
        {
            int eventIndex = _listView.selectedIndex;
            onEventSelected?.Invoke(_events[eventIndex]);
        }
    }
}
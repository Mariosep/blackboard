using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blackboard.Requirement;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public class SimplifiedRequirementsListView : VisualElement
    {
        public new class UxmlFactory: UxmlFactory<SimplifiedRequirementsListView, UxmlTraits> { }
        
        private readonly string uxmlName = "UXML/SimplifiedRequirementsListView.uxml";

        public Action<float> onMinFactLabelWidthModified;
        
        // Data
        private RequirementsSO requirementsData;
        private List<ConditionSO> conditions => requirementsData.conditions;

        public float minFactLabelWidth = 20;
        
        // Visual elements
        private ListView listView;
        
        public SimplifiedRequirementsListView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlName);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
        
            listView = this.Q<ListView>();
        
            Setup();
        }
        
        private void Setup()
        {
            listView.makeItem = MakeItem;
            listView.bindItem = (element, i) => BindItem(element, i);
        }
        
        public void PopulateView(RequirementsSO requirements)
        {
            this.requirementsData = requirements;
            listView.itemsSource = conditions;
            listView.bindingPath = "conditions";

            this.requirementsData.onRequirementsModified += UpdateContent;
        }

        public void UpdateMinFactLabelWidth(float minWidth)
        {
            minFactLabelWidth = minWidth;
            
            onMinFactLabelWidthModified?.Invoke(minFactLabelWidth);
        }
        
        private VisualElement MakeItem() => new VisualElement();

        private void BindItem(VisualElement element, int i)
        {
            element.Clear();

            var conditionView = SimplifiedConditionViewFactory.CreateConditionView(conditions[i]);
            conditionView.BindCondition(this, conditions[i]);
            
            element.Add(conditionView);
        }

        private void UpdateContent()
        {
            listView.RefreshItems();
        }

        #region Runtime

        public void EnableDebugMode(Requirements req)
        {
            for (int i = 0; i < req.conditions.Count; i++)
            {
                var list = listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
                var item = list.ElementAt(i).Q<SimplifiedConditionView>();
                item.EnableDebugMode(req.conditions[i]);
            }
        }
        
        public void DisableDebugMode()
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                var list = listView.Query<VisualElement>(className: "unity-list-view__item").ToList();
                var item = list.ElementAt(i).Q<SimplifiedConditionView>();
                item.DisableDebugMode();
            }
        }
        
        #endregion
    }
}
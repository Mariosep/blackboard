using System;
using System.IO;
using Blackboard.Facts;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public class FactView : VisualElement
    {
        public Action<FactSO> onFactSelected;
    
        protected FactDropdown factDropdown;
        protected VisualElement valueContainer;

        protected bool showValue;

        protected virtual string ValueViewUxmlPath => null;

        public FactView(bool showValue = false)
        {
            factDropdown = new FactDropdown();
            factDropdown.onFactSelected += SetFact;
        
            factDropdown.style.marginTop = 5;
            factDropdown.style.marginBottom = 5;
        
            Add(factDropdown);

            this.showValue = showValue;
        
            if(showValue)
                AddValueField();
        }

        public void SetName(string factName)
        {
            factDropdown.SetName(factName.Capitalize());
        }

        protected void ShowValue()
        {
            valueContainer.visible = true;
        }

        public virtual void SetFact(FactSO factSelected) {}
    
        protected virtual void AddValueField()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, ValueViewUxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(factDropdown);

            valueContainer = factDropdown.Q<VisualElement>("value-content");
            valueContainer.SetEnabled(false);
            valueContainer.visible = false;
        
            valueContainer.style.marginTop = 5;
        }
    }
}
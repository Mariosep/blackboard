using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Events
{
    public class EventCategoryHeaderView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<EventCategoryHeaderView, UxmlTraits> { }
    
        private readonly string uxmlPath = "UXML/EventCategoryHeader.uxml";

        // Visual elements
        private Label categoryLabel;
        
        public EventCategoryHeaderView()
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);
            
            categoryLabel = this.Q<Label>("category__label");
        }

        public void PopulateView(string category)
        {
            categoryLabel.text = category;
        }
    }
}
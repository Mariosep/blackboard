using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor
{
    public class BindableItem<T> : VisualElement where T : BindableElement
    {
        private BindableElement bindableElement;
        
        public BindableItem(string bindingPath, string uxmlPath)
        {
            string path = Path.Combine(BlackboardEditorWindow.RelativePath, uxmlPath);
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(this);

            bindableElement = this.Q<T>();
            bindableElement.bindingPath = bindingPath;
            bindableElement.name = bindingPath;
        }
       
        public void SetDataSource(BlackboardElementSO dataSource)
        {
            var so = new SerializedObject(dataSource);
            bindableElement.Bind(so);
        }
    }
}
using Blackboard.Facts;
using UnityEditor;
using UnityEditor.UIElements;

namespace Blackboard.Editor.Facts
{
    public class FactGroupListView : GroupListView
    {
        private FactDataBaseSO _factDataBase;

        protected override void Setup()
        {
            _listView.makeItem = MakeItem;
            _listView.bindItem = (element, i) => BindItem(element, new SerializedObject(_factDataBase.groupsList[i]));
        }

        public void PopulateView(FactDataBaseSO factDataBase)
        {
            _factDataBase = factDataBase;
            _listView.bindingPath = "groupsList";
            _listView.Bind(new SerializedObject(_factDataBase));
            _listView.itemsSource = _factDataBase.groupsList;

            SetSelection(0);
        }
    }
}
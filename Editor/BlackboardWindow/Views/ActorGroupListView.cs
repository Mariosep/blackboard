using UnityEditor;
using UnityEditor.UIElements;

public class ActorGroupListView : GroupListView
{
    private ActorDataBaseSO _actorDataBase;
    
    protected override void Setup()
    {
        _listView.makeItem = MakeItem;
        _listView.bindItem = (element, i) => BindItem(element, new SerializedObject(_actorDataBase.groupsList[i]));
    }
    
    public void PopulateView(ActorDataBaseSO actorDataBase)
    {
        _actorDataBase = actorDataBase;
        _listView.bindingPath = "groupsList";
        _listView.Bind(new SerializedObject(_actorDataBase));
        _listView.itemsSource = _actorDataBase.groupsList;
        
        if(actorDataBase.groupsList.Count > 0)
            SetSelection(0);
    }
}
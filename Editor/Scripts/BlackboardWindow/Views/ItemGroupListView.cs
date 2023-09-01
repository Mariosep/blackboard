public class ItemGroupListView : GroupListView
{
    //private ItemDataBaseSO _itemDataBase;
    
    /*protected override void Setup()
    {
        _listView.makeItem = MakeItem;
        _listView.bindItem = (element, i) => BindItem(element, new SerializedObject(_dataBase.GroupList[i]));
    }*/
    
    /*public void PopulateView(ItemDataBaseSO itemDataBase)
    {
        _itemDataBase = itemDataBase;
        
        if(_itemDataBase.groupsList.Count > 0)
        {
            _listView.itemsSource = _itemDataBase.groupsList;
            
            SetSelection(0);
        }
    }*/

    /*public void AddGroup(ItemGroupSO itemGroup)
    {
        _itemDataBase.AddGroup(itemGroup);
        
        if(_itemDataBase.groupsList.Count == 1)
        {
            _listView.itemsSource = _itemDataBase.groupsList;
            _listView.RefreshItems();
            _listView.visible = true;
            
            SetSelection(0);
        }
        
        _listView.RefreshItems();
    }*/
    
    /*public void RemoveGroup(string id)
    {
        _itemDataBase.RemoveGroup(id);
        
        _listView.RefreshItems();

        if (_itemDataBase.groupsList.Count == 0)
        {
            _listView.visible = false;
        }
        else
            SetSelection(0);    
    }*/
}
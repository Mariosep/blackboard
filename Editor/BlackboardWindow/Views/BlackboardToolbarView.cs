using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class BlackboardToolbarView : Toolbar
{
    public new class UxmlFactory : UxmlFactory<BlackboardToolbarView, UxmlTraits> { }

    public System.Action onCreateBlackboardClicked;

    private ToolbarMenu _assetsMenu;
    
    public BlackboardToolbarView()
    {
        _assetsMenu = new ToolbarMenu()
        {
            text = "Assets"
        };
        
        AddMenuActions();
        
        this.Add(_assetsMenu);
    }

    private void AddMenuActions()
    {
        _assetsMenu.menu.AppendAction("Create blackboard", (x) => onCreateBlackboardClicked?.Invoke());
    }
}
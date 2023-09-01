using System;
using UnityEditor;
using UnityEngine;

public static class FactPopUpMenu
{
    public static Action<FactType> onPopupMenuOptionSelected;
    
    public static void DisplayAddFactPopupMenu()
    {
        Event evt = Event.current;

        float width = 100f;
        float height = 100f;
        
        Rect rectPos = new Rect(evt.mousePosition.x - width, evt.mousePosition.y - 90, width, height);
        
        EditorUtility.DisplayPopupMenu(rectPos, "Facts/", null);
    }
    
    [MenuItem("Facts/Bool")]
    private static void CreateBoolFact()
    {
        onPopupMenuOptionSelected?.Invoke(FactType.Bool);
    }
    
    [MenuItem("Facts/Integer")]
    private static void CreateIntegerFact()
    {
        onPopupMenuOptionSelected?.Invoke(FactType.Int);
    }
    
    [MenuItem("Facts/Float")]
    private static void CreateFloatFact()
    {
        onPopupMenuOptionSelected?.Invoke(FactType.Float);
    }
    
    [MenuItem("Facts/String")]
    private static void CreateStringFact()
    {
        onPopupMenuOptionSelected?.Invoke(FactType.String);
    }
}
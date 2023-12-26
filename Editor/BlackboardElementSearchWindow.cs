using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Blackboard
{
    public class BlackboardElementSearchWindow
    {
        public static void Open(Action<BlackboardElementSO> callback, List<BlackboardElementType> typesAllowed)
        {
            var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            
            List<KeyValuePair<FactSO, string>> factPairs = null;
            List<KeyValuePair<EventSO, string>> eventPairs = null;
            List<KeyValuePair<ActorSO, string>> actorPairs = null;
            List<KeyValuePair<ItemSO, string>> itemPairs = null;
        
            if(typesAllowed.Contains(BlackboardElementType.Fact))
                factPairs = BlackboardEditorManager.instance.FactDataBase.GetPairs();
            if(typesAllowed.Contains(BlackboardElementType.Event))
                eventPairs = BlackboardEditorManager.instance.EventDataBase.GetPairs();
            if(typesAllowed.Contains(BlackboardElementType.Actor))
                actorPairs = BlackboardEditorManager.instance.ActorDataBase.GetPairs();
            if(typesAllowed.Contains(BlackboardElementType.Item))
                itemPairs = BlackboardEditorManager.instance.ItemDataBase.GetPairs();

            var blackboardElementSearchProvider = ScriptableObject.CreateInstance<BlackboardElementSearchProvider>();
            blackboardElementSearchProvider.Init(callback, factPairs, eventPairs, actorPairs, itemPairs);
        
            SearchWindow.Open(new SearchWindowContext(mousePos), blackboardElementSearchProvider);
        }
    }    
}


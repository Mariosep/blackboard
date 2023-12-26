using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Blackboard.Actions
{
    public class BlackboardActionSearchWindow
    {
        private static List<KeyValuePair<Type, string>> _actionPairs;

        public static void Open(Action<Type> callback)
        {
            var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);

            var actionPairs = GetActionPairs();

            var blackboardActionSearchProvider = ScriptableObject.CreateInstance<BlackboardActionSearchProvider>();
            blackboardActionSearchProvider.Init(actionPairs, callback);

            SearchWindow.Open(new SearchWindowContext(mousePos), blackboardActionSearchProvider);
        }

        private static List<KeyValuePair<Type, string>> GetActionPairs()
        {
            /*if (_actionPairs != null)
                return _actionPairs;*/

            IEnumerable<Type> GetAllActionTypes()
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(Action)));
            }

            _actionPairs = new List<KeyValuePair<Type, string>>();

            var actionTypes = GetAllActionTypes();

            foreach (Type actionType in actionTypes)
            {
                string actionName = Regex.Replace(actionType.Name, "([a-z])([A-Z])", "$1 $2")
                    .Replace("Action", "");

                CategoryAttribute categoryAttribute = actionType.GetCustomAttribute<CategoryAttribute>();
                string actionPath = categoryAttribute != null
                    ? $"{categoryAttribute.Category}/{actionName}"
                    : actionName;

                var actionPair = new KeyValuePair<Type, string>(actionType, actionPath);
                _actionPairs.Add(actionPair);
            }

            return _actionPairs;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Blackboard.Commands;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Blackboard.Editor.Commands
{
    public class CommandSearchWindow
    {
        private static List<KeyValuePair<Type, string>> _commandPairs;

        public static void Open(Action<Type> callback)
        {
            var mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);

            var commandPairs = GetCommandPairs();

            var commandSearchProvider = ScriptableObject.CreateInstance<CommandSearchProvider>();
            commandSearchProvider.Init(commandPairs, callback);

            SearchWindow.Open(new SearchWindowContext(mousePos), commandSearchProvider);
        }

        private static List<KeyValuePair<Type, string>> GetCommandPairs()
        {
            IEnumerable<Type> GetAllCommandTypes()
            {
                return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsSubclassOf(typeof(Command)));
            }

            _commandPairs = new List<KeyValuePair<Type, string>>();

            var commandTypes = GetAllCommandTypes();

            foreach (Type commandType in commandTypes)
            {
                string commandName = Regex.Replace(commandType.Name, "([a-z])([A-Z])", "$1 $2")
                    .Replace("Command", "");

                CategoryAttribute categoryAttribute = commandType.GetCustomAttribute<CategoryAttribute>();
                string commandPath = categoryAttribute != null
                    ? $"{categoryAttribute.Category}/{commandName}"
                    : commandName;

                var commandPair = new KeyValuePair<Type, string>(commandType, commandPath);
                _commandPairs.Add(commandPair);
            }

            return _commandPairs;
        }
    }
}
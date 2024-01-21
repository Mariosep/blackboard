using System;
using System.Reflection;
using UnityEditor;

namespace Blackboard.Editor
{
    public static class SerializedPropertyUtility
    {
        public static Object GetPropertyInstance(SerializedProperty property) {       

            string path = property.propertyPath;

            Object obj = property.serializedObject.targetObject;
            var type = obj.GetType();

            var fieldNames = path.Split('.');
            for (int i = 0; i < fieldNames.Length; i++) {
                var info = type.GetField(fieldNames[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (info == null)
                    break;

                // Recurse down to the next nested object.
                obj = info.GetValue(obj);
                type = info.FieldType;            
            }

            return obj;
        }
    }
}
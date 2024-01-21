using System;
using System.Reflection;

public static class TypeExtension
{
    public static string GetName(this Type t)
    {
        string name = t.Name;

        return name switch
        {
            "String" => "string",
            "Int32" => "int",
            _ => name
        };
    }
    
    public static FieldInfo GetFieldInfo(this Type t, string fieldName)
    {
        FieldInfo fieldInfo = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public);
        return fieldInfo;
    }
    
    public static object GetFieldValue(this Type t, object instance, string fieldName)
    {
        FieldInfo fieldInfo = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public);

        if (fieldInfo != null)
        {
            // Get the field value
            object fieldValue = fieldInfo.GetValue(instance);
            return fieldValue;
        }
        else
        {
            Console.WriteLine($"Field '{fieldName}' not found");
        }
        
        return null;
    }
}
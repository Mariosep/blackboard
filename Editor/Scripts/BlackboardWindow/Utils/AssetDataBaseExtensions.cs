using System.IO;
using UnityEditor;
using UnityEngine;

public static class AssetDataBaseExtensions
{
    public static string GetPath(string filter)
    {
        string[] guids = AssetDatabase.FindAssets(filter);
        
        if (guids == null || guids.Length == 0)
            return null;

        return AssetDatabase.GUIDToAssetPath(guids[0]);
    }

    public static string GetPathToScript<T>() where T : class
    {
        return GetPath($"t:Script {typeof(T).Name}");
    }

    public static string GetPathToScriptableObject<T>() where T : ScriptableObject
    {
        return GetPath($"t:{typeof(T).Name}");
    }
    
    public static string GetDirectoryOfScript<T>() where T : class
    {
        string relativePath = GetPathToScript<T>();
        
        return string.IsNullOrEmpty(relativePath) ? null : Path.GetDirectoryName(relativePath);
    }
}
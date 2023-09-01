using System.IO;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectUtility
{
    public static void SaveAsset(ScriptableObject asset, string path)
    {
        string dirPath = Path.GetDirectoryName(path);
        string assetName = Path.GetFileNameWithoutExtension(path);
        
        if (!AssetDatabase.IsValidFolder(dirPath))
        {
            string dirPathWithoutAssets = dirPath.Replace("Assets\\", "");
            Directory.CreateDirectory(Path.Combine(Application.dataPath, dirPathWithoutAssets));
        }

        path = Path.Combine(dirPath, assetName);
        path = Path.ChangeExtension(path, ".asset");
        
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void SaveSubAsset(ScriptableObject asset, ScriptableObject mainAsset)
    {
        AssetDatabase.AddObjectToAsset(asset, mainAsset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static T Load<T>(string path) where T : ScriptableObject
    {
        return AssetDatabase.LoadAssetAtPath<T>(path);
    }

    public static void DeleteAsset(params ScriptableObject[] assets)
    {
        foreach (ScriptableObject asset in assets)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            AssetDatabase.DeleteAsset(assetPath);    
        }
    }
    
    public static void DeleteSubAsset(params ScriptableObject[] assets)
    {
        foreach (ScriptableObject asset in assets)
        {
            Object.DestroyImmediate(asset, true);    
        }
    }
}
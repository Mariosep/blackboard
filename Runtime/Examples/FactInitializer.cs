using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public static class FactInitializer
{
    public static System.Action onStart;
    public static System.Action onExit;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Setup()
    {
        //onStart?.Invoke();

        EditorApplication.playModeStateChanged += OnDestroy;
    }

    private static void OnDestroy(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.ExitingPlayMode)
        {
            onExit?.Invoke();
            
            EditorApplication.playModeStateChanged -= OnDestroy;
        }
    }
}
#endif
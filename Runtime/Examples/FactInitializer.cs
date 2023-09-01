using System;
using UnityEditor;
using UnityEngine;

public static class FactInitializer
{
    public static Action onStart;
    public static Action onExit;

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
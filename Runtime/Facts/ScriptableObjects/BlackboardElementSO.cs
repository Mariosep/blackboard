using UnityEditor;
using UnityEngine;

public abstract class BlackboardElementSO : ScriptableObject
{
    public System.Action onNameChanged;
    
    public string id;
    public string groupId;
    public string theName;
    public string description;

    public BlackboardElementType blackboardElementType;

    private string originalState;
    
    public void SetName(string newName)
    {
        if(newName == theName)
            return;
        
        theName = newName;
        onNameChanged?.Invoke();
    }
    
    private void SaveState()
    {
        originalState = JsonUtility.ToJson(this);
    }

    private void RevertChanges()
    {
        JsonUtility.FromJsonOverwrite(originalState, this);
    }
    
    public virtual void OnEnable()
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
    }
    
    public virtual void OnDisable()
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
    }
    
#if UNITY_EDITOR
    private void OnPlayModeStateChanged(PlayModeStateChange playModeState)
    {
        switch(playModeState)
        {
            case PlayModeStateChange.EnteredPlayMode:
                SaveState();
                break;

            case PlayModeStateChange.ExitingPlayMode:
                RevertChanges();
                break;
        }
    }
#endif
}

public enum BlackboardElementType
{
    Fact,
    Event,
    Actor,
    Item
}
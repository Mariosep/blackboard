using System;
using Unity.Properties;
using UnityEditor;
using UnityEngine;

namespace Blackboard
{
    public abstract class BlackboardElementSO : ScriptableObject
    {
        public Action onNameChanged;

        public string id;
        public ElementGroupSO group;
        public string theName;
        public string description;

        public virtual BlackboardElementType BlackboardElementType => BlackboardElementType.Fact;

        private string originalState;

        [CreateProperty]
        public string Name
        {
            get => theName;
            set
            {
                //value = BlackboardValidator.ValidateName(value, this);
                
                if (value == theName)
                    return;

                theName = value;
                
                onNameChanged?.Invoke();
            }
        }
        
        [CreateProperty]
        public string Description
        {
            get => description;
            set
            {
                //value = BlackboardValidator.ValidateDescription(value, this);
                description = value;
            }
        }

        public abstract void Init(string id);
        
        public void SaveState()
        {
            originalState = JsonUtility.ToJson(this);
        }

        public void RevertChanges()
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
            switch (playModeState)
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

        public override string ToString()
        {
            return theName;
        }
    }

    public enum BlackboardElementType
    {
        Fact,
        Event,
        Actor,
        Item,
    }
}
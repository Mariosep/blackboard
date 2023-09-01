using System;
using UnityEngine;

[Serializable]
public abstract class BlackboardEvent<T1, T2> 
    where T1 : EventSO
    where T2 : ScriptableObject
{
    public string id;
    public string categoryId;
    public BlackboardEventType eventType;
    
    [SerializeField] protected T1 eventSO;

    public T2 arg;
    
    protected Action<T2> currentListener;
    
    public bool HasEvent => eventSO != null;
    public bool HasArg => arg != null;
    
    public void SetEvent(T1 eventSo)
    {
        if(Application.isPlaying)
        {
            var auxListener = currentListener;

            if (eventSO != null)
                RemoveListener(auxListener);

            this.eventSO = eventSo;

            AddListener(auxListener);
        }
        else
        {
            this.eventSO = eventSo;
        }
    }

    public abstract void AddListener(Action<T2> listener);
    public abstract void RemoveListener(Action<T2> listener);

    public void OnEventInvoked(T2 eventArg)
    {
        if (eventArg == arg)
        {
            currentListener?.Invoke(eventArg);
        }
    }
}
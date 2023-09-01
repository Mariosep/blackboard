using System;
using UnityEngine;

[Serializable]
public abstract class Fact<T1,T2> where T1 : FactSO
{
    private Action<T2> _onValueChanged;
    
    public Action<T2> onValueChanged
    {
        get => _onValueChanged;
        set
        {
            _onValueChanged = value;
            
            if(value != null)
                Setup();
        }
    }

    public string id;
    public string categoryId;
    public FactType factType;
    
    [SerializeField] protected T1 fact;
    
    public bool HasValue => fact != null;

    public abstract void Setup();
    public abstract void OnDestroy();
    
    public virtual void OnValueChanged(T2 newValue)
    {
        _onValueChanged?.Invoke(newValue);
    }
}
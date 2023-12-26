using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "InteractionData", fileName = "InteractionData")]
public class InteractionsSO : ScriptableObject
{
    public List<InteractionEventTrigger> interactions = new List<InteractionEventTrigger>();
    
    public void Init()
    {
        var interfaceType = typeof(IInteractable);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p) && !p.IsInterface);

        foreach (Type type in types)
        {
            if(interactions.Any(i => i.interactableName == type.Name))
                continue;
            
            interactions.Add(new InteractionEventTrigger(type.Name));            
        }
    }
}

[Serializable]
public class InteractionEventTrigger
{
    public string interactableName;
    public EventSO eventToTrigger;

    public InteractionEventTrigger(string interactableName)
    {
        this.interactableName = interactableName;
        eventToTrigger = null;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class ElementGroupSO<T> : ScriptableObject where T : BlackboardElementSO
{
    public string id;
    public string groupName;
    
    public List<T> elementsList = new List<T>();
    
    [SerializedDictionary("ID", "Element")]
    public SerializedDictionary<string, T> elementsDic = new SerializedDictionary<string, T>();
    
    public void SetName(string newGroupName)
    {
        groupName = newGroupName;
    }
    
    public void AddElement(T element)
    {
        element.groupId = id;
        
        elementsList.Add(element);
        elementsDic.Add(element.id, element);
    }

    public void RemoveElement(string id)
    {
        if (elementsDic.TryGetValue(id, out T element))
        {
            element.groupId = "";
            
            elementsList.Remove(element);
            elementsDic.Remove(id);
        }
        else
            throw new Exception("Can not remove element because is not registered in the blackboard");
    }

    public void Replace(int i, T element)
    {
        elementsList[i] = element;
        elementsDic[element.id] = element;
    }
    
    public T GetElementByID(string id)
    {
        if (elementsDic.TryGetValue(id, out T element))
            return element;
        else
            throw new Exception("Element is not registered in the blackboard");
    }
    
    public T GetElementByName(string elementName)
    {
        var elements = elementsList.Where(e => e.theName == elementName).ToList(); 
        
        if (elements.Count > 0)
            return elements[0];
        else
            throw new Exception("Element is not registered in the blackboard");
    }
    
    public bool ContainsElementWithName(string elementName, T elementToIgnore = null)
    {
        var elements = elementsList.Where(e => e.theName == elementName).ToList();
        
        if (elementToIgnore != null && elements.Contains(elementToIgnore))
            elements.Remove(elementToIgnore);

        return elements.Count > 0;
    }

    public bool ContainsElement(T element)
    {
        return elementsList.Contains(element);
    }
    
    public bool TryGetElement(string id, out T element)
    {
        if (elementsDic.TryGetValue(id, out element))
            return true;
        else
            return false;
    }
}
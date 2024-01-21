using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Blackboard
{
    public abstract class ElementGroupSO : ScriptableObject
    {
        public string id;
        public string groupName;
        
        public abstract bool ContainsElementWithName(string elementName, BlackboardElementSO elementToIgnore = null);
    }
    
    public abstract class ElementGroupSO<T> : ElementGroupSO where T : BlackboardElementSO
    {
        public List<T> elementsList = new List<T>();
    
        [SerializedDictionary("ID", "Element")]
        public SerializedDictionary<string, T> elementsDic = new SerializedDictionary<string, T>();
    
        public void SetName(string newGroupName)
        {
            groupName = newGroupName;
        }
    
        public void AddElement(T element)
        {
            element.group = this;
        
            elementsList.Add(element);
            elementsDic.Add(element.id, element);
        }

        public void RemoveElement(string id)
        {
            if (elementsDic.TryGetValue(id, out T element))
            {
                element.group = null;
            
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
            var elements = elementsList.Where(e => e.Name == elementName).ToList(); 
        
            if (elements.Count > 0)
                return elements[0];
            else
                throw new Exception("Element is not registered in the blackboard");
        }

        public void SaveState()
        {
            foreach (var element in elementsList)
            {
                element.SaveState();
            }        
        }
    
        public void RevertChanges()
        {
            foreach (var element in elementsList)
            {
                element.RevertChanges();
            }        
        }
    
        public override bool ContainsElementWithName(string elementName, BlackboardElementSO elementToIgnore = null)
        {
            var elements = elementsList.Where(e => e.Name == elementName).ToList();
        
            if (elementToIgnore != null && elements.Contains(elementToIgnore))
                return elements.Count > 1;
            else
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
}
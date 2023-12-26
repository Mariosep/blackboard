using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public static class BlackboardValidator
{
    public static bool ValidateBlackboardName(string blackboardName, VisualElement inputText, Label validationlabel)
    {
        if (blackboardName == "")
        {
            inputText.AddToClassList("input--invalid");
            validationlabel.text = "Blackboard name can't be empty";
                        
            return false;
        }

        return true;
    }
    
    /*public static bool GetValidFactName(ref string newName, FactGroupSO factGroup)
    {
        newName = newName.Trim();
        bool isValid = ValidateFormatName(newName, fact.theName);
            
        if(isValid)
        {
            newName = GetValidName(factGroup, newName, fact.theName);
        }

        return isValid;
    }*/
    
    public static bool TrySetEventName(ref string newName, EventSO eventSo, EventGroupSO eventGroup)
    {
        newName = newName.Trim();
        bool isValid = ValidateFormatName(newName, eventSo.theName);
            
        if(isValid)
        {
            //newName = GetValidName(eventGroup, newName, eventSo.theName);
        }

        return isValid;
    }
    
    public static bool TrySetActorName(ref string newName, ActorSO actor, ActorGroupSO actorGroup)
    {
        newName = newName.Trim();
        bool isValid = ValidateFormatName(newName, actor.theName);
            
        if(isValid)
        {
            //newName = GetValidName(actorGroup, newName, actor.theName);
        }

        return isValid;
    }
    
    public static bool TrySetItemName(ref string newName, ItemSO item, ItemGroupSO itemGroup)
    {
        newName = newName.Trim();
        bool isValid = ValidateFormatName(newName, item.theName);
            
        if(isValid)
        {
            //newName = GetValidName(itemGroup, newName, item.theName);
        }

        return isValid;
    }
    
    public static bool ValidateFormatName(string newName, string currentName)
    {
        if(newName == "")
        {
            Debug.Log("Name can't be empty");
            return false;
        }
        if(newName == currentName)
        {
            return false;
        }

        return true;
    }

    public static string GetValidName<T>(ElementGroupSO<T> elementGroup, string newName, T element, bool isNewElement = false) where T : BlackboardElementSO
    {
        newName = newName.Trim();
        
        if(!isNewElement && newName == element.theName)
            return element.theName;
            
        if(newName == "")
            return element.theName;

        if(Regex.IsMatch(newName, "_[0-9]$") && elementGroup.ContainsElementWithName(newName, element))
            newName = Regex.Replace(newName, "_[0-9]$", "");
        
        string newNameWithNumber = newName;
        
        int count = 0;
        while (elementGroup.ContainsElementWithName(newNameWithNumber, element))
        {
            count++;
            newNameWithNumber = $"{newName}_{count}";
        }

        return newNameWithNumber;
    }
}

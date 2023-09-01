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

        /*if (blackboardExists)
        {
            inputText.AddToClassList("input--invalid");
            validationlabel.text = "Group name already exists";
                        
            return false;
        }

        return true;*/
    }

    public static bool ValidateElementName(string newName, string currentName)
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
}

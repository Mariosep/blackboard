using UnityEngine.UIElements;

public static class GroupValidator
{
    public static bool Validate(string groupName, VisualElement inputText, Label validationlabel, BlackboardElementType blackboardElementType)
    {
        if (groupName == "")
        {
            inputText.AddToClassList("input--invalid");
            validationlabel.text = "Group name can't be empty";
                        
            return false;
        }

        bool groupExists = false;
        
        switch (blackboardElementType)
        {
            case BlackboardElementType.Fact:
                groupExists = BlackboardManager.instance.FactDataBase.GroupExists(groupName);
                break;
            
            case BlackboardElementType.Event:
                groupExists = BlackboardManager.instance.EventDataBase.GroupExists(groupName);
                break;
            
            case BlackboardElementType.Actor:
                groupExists = BlackboardManager.instance.ActorDataBase.GroupExists(groupName);
                break;
            
            case BlackboardElementType.Item:
                groupExists = BlackboardManager.instance.ItemDataBase.GroupExists(groupName);
                break;
        }

        if (groupExists)
        {
            inputText.AddToClassList("input--invalid");
            validationlabel.text = "Group name already exists";
                        
            return false;
        }

        return true;
    }
}
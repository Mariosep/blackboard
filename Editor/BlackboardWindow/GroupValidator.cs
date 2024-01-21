using UnityEngine.UIElements;

namespace Blackboard.Editor
{
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
                    groupExists = BlackboardEditorManager.instance.FactDataBase.GroupExists(groupName);
                    break;
            
                case BlackboardElementType.Event:
                    //groupExists = BlackboardEditorManager.instance.EventDataBase.GroupExists(groupName);
                    break;
            
                case BlackboardElementType.Actor:
                    groupExists = BlackboardEditorManager.instance.ActorDataBase.GroupExists(groupName);
                    break;
            
                case BlackboardElementType.Item:
                    groupExists = BlackboardEditorManager.instance.ItemDataBase.GroupExists(groupName);
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
}
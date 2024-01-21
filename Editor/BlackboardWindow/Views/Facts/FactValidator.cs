using Blackboard.Facts;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public static class FactValidator
    {
        public static void ValidateAndSetName(FactGroupSO factGroup, TextField nameField, FactSO fact)
        {
            string validName = BlackboardValidator.GetValidName(factGroup, nameField.value, fact);
            fact.SetName(validName);
            nameField.value = fact.theName;
        }
    }
}
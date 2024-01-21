using System.Text.RegularExpressions;
using Blackboard.Facts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Blackboard
{
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

        public static string GetValidName(ElementGroupSO elementGroup, string previousName, string newName, BlackboardElementSO element, bool isNewElement = false)
        {
            newName = newName.Trim();
        
            if(!isNewElement && newName == previousName)
                return previousName;
            
            if(newName == "")
                return previousName;

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

        public static void ValidateAndSetName<T>(string previousName, string newName, T element) where T : BlackboardElementSO
        {
            string validName = GetValidName(element.group, previousName, newName, element);
            element.Name = validName;
        }

        public static void ValidateAndSetDescription(string previousDescription, string newDescription, BlackboardElementSO element)
        {
            newDescription = newDescription.Trim();
            element.Description = newDescription;
        }
        
        public static void ValidateAndSetValue(string newValue, StringFactSO stringFact)
        {
            newValue = newValue.Trim();
            stringFact.Value = newValue;
        }
        
        public static void ValidateAndSetValue(int newValue, IntFactSO intFact)
        {
            intFact.Value = newValue;
        }
        
        public static void ValidateAndSetValue(float newValue, FloatFactSO floatFact)
        {
            floatFact.Value = newValue;
        }
        
        public static void ValidateAndSetValue(bool newValue, BoolFactSO boolFact)
        {
            boolFact.Value = newValue;
        }
    }
}
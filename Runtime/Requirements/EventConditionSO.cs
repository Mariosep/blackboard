using System.Collections.Generic;
using Blackboard.Events;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Requirement
{
    public class EventConditionSO : ConditionSO
    {
        public BaseEventSO eventRequired;
        public List<bool> paramValuesRequired;
        //public int timesRequired;

        public override void Init(string id)
        {
            base.Init(id);

            type = ConditionType.Event;
        }

        public override void SetElementRequired(BlackboardElementSO elementRequired)
        {
            if (elementRequired is BaseEventSO eventRequired)
            {
                this.eventRequired = eventRequired;
                paramValuesRequired = new List<bool>(eventRequired.ParametersCount);
                paramValuesRequired.AddRange(new bool[eventRequired.ParametersCount]);
            }
        }

        public override BlackboardElementSO GetElementRequired()
        {
            return eventRequired;
        }

        public override string ToString()
        {
            string eventString = eventRequired.EventName;

            string paramValueString = "";
            
            switch (eventRequired.ParametersCount)
            {
                case 1:
                    if (paramValuesRequired[0])
                    {
                        string paramName = eventRequired.eventInfo.parameters[0].parameterName;
                        paramValueString = eventRequired.GetParameterValue(0).ToString();
                        eventString += $"\n \t{paramName}: {paramValueString}";
                    }
                    break;
                
                case 2:
                    if (paramValuesRequired[0])
                    {
                        string paramName = eventRequired.eventInfo.parameters[0].parameterName;
                        paramValueString = eventRequired.GetParameterValue(0).ToString();
                        eventString += $"\n \t{paramName}: {paramValueString}";
                    }
                    
                    if (paramValuesRequired[1])
                    {
                        string paramName = eventRequired.eventInfo.parameters[1].parameterName;
                        paramValueString = eventRequired.GetParameterValue(1).ToString();
                        eventString += $"\n \t{paramName}: {paramValueString}";
                    }
                    break;
            }
            return eventString;
        }
        
        public override void SaveAs(ScriptableObject mainAsset)
        {
#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(this, mainAsset);
            AssetDatabase.AddObjectToAsset(eventRequired, mainAsset);
            AssetDatabase.SaveAssets();
#endif
        }
        
    }
}
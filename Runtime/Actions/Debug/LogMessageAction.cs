using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("Log Message")]
    public class LogMessageAction : Action
    {
        public string messageToLog;

        public override string GetName() => "Log Message";

        public override IEnumerator Execute()
        {
            Debug.Log(messageToLog);
            
            //return Task.CompletedTask;
            yield return null;
        }
    }    
}

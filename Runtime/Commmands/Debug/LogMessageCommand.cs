using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Log Message")]
    public class LogMessageCommand : Command
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

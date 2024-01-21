using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Time")]
    public class WaitSecondsCommand : Command
    {
        public float secondsToWait;
        public override string GetName() => "Wait Seconds";

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(secondsToWait);
        }
    }
}
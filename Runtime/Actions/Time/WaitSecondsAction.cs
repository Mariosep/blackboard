using System.Collections;
using UnityEngine;

namespace Blackboard.Actions
{
    [Category("Time")]
    public class WaitSecondsAction : Action
    {
        public float secondsToWait;
        public override string GetName() => "Wait Seconds";

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(secondsToWait);
        }
    }
}
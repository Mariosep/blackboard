using System.Collections;
using UnityEngine;

namespace Blackboard.Commands
{
    [Category("Audio")]
    public class PlayAudioClipCommand : Command
    {
        public AudioClip audioClip;
        
        public override string GetName() => "Play Audio Clip";

        public override IEnumerator Execute()
        {
            Debug.Log(GetName());

            if (audioClip != null)
                AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
            
            yield return null;
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Blackboard.Commands
{
    [Category("Sequencing")]
    public class PlayTimelineSceneCommand : Command
    {
        public PlayableDirector playableDirector;
        public PlayableAsset timelineScene;
        
        public override string GetName() => "Play Timeline Scene";

        public override IEnumerator Execute()
        {
            if (playableDirector != null && timelineScene != null)
            {
                playableDirector.Play(timelineScene);
                yield return new WaitForSeconds((float) timelineScene.duration);
            }
            else
                yield return null;
        }
    }
}
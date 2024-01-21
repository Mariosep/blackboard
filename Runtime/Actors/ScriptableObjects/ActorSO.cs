using UnityEngine;

namespace Blackboard.Actors
{
    public class ActorSO : BlackboardElementSO
    {
        public string shortName;
        public GameObject prefab;
        public Texture2D icon;
    
        public override BlackboardElementType BlackboardElementType => BlackboardElementType.Actor;
        
        public override void Init(string id)
        {
            this.id = id;
            name = $"actor-{this.id}";
            theName = "NewActor";
            hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
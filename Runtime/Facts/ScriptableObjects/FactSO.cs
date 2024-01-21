using UnityEngine;

namespace Blackboard.Facts
{
    public abstract class FactSO : BlackboardElementSO
    {
        public FactType type;

        public override BlackboardElementType BlackboardElementType => BlackboardElementType.Fact;
        
        public override void Init(string id)
        {
            this.id = id;
            name = $"fact-{this.id}";

            switch (type)
            {
                case FactType.Bool:
                    theName = "NewBool";
                    break;
            
                case FactType.Float:
                    theName = "NewFloat";
                    break;
            
                case FactType.Int:
                    theName = "NewInt";
                    break;
            
                case FactType.String:
                    theName = "NewString";
                    break;
            }
            hideFlags = HideFlags.HideInHierarchy;
        }
    }
    
    public enum FactType
    {
        Bool,
        Int,
        Float,
        String
    }
}


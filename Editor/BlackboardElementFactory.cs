using System;
using Blackboard.Actors;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Editor
{
    public static class BlackboardElementFactory
    { 
        public static FactSO CreateFact(FactType type, string id = null)
        {
            FactSO fact = type switch
            {
                FactType.Bool => ScriptableObject.CreateInstance<BoolFactSO>(),
                FactType.Int => ScriptableObject.CreateInstance<IntFactSO>(),
                FactType.Float => ScriptableObject.CreateInstance<FloatFactSO>(),
                FactType.String => ScriptableObject.CreateInstance<StringFactSO>(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            
            if(id == null)
                id = GUID.Generate().ToString();
            
            fact.Init(id);
                
            return fact;
        }
        
        public static ActorSO CreateActor(string id = null)
        {
            ActorSO actor = ScriptableObject.CreateInstance<ActorSO>();

            if(id == null)
                id = GUID.Generate().ToString();
        
            actor.Init(id);
            return actor;
        }
    
        public static ItemSO CreateItem(string id = null)
        {
            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();

            if(id == null)
                id = GUID.Generate().ToString();
        
            item.Init(id);
        
            return item;
        }
    }
}
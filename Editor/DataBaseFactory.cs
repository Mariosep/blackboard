using Blackboard.Actors;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEditor;
using UnityEngine;

namespace Blackboard.Editor
{
    public static class DataBaseFactory
    {
        public static FactDataBaseSO CreateFactDataBase()
        {
            FactDataBaseSO factDataBase = ScriptableObject.CreateInstance<FactDataBaseSO>();
            factDataBase.id = GUID.Generate().ToString();
            factDataBase.name = $"factDB-{factDataBase.id}";
            factDataBase.hideFlags = HideFlags.HideInHierarchy;

            return factDataBase;
        }

        public static ActorDataBaseSO CreateActorDataBase()
        {
            ActorDataBaseSO actorDataBase = ScriptableObject.CreateInstance<ActorDataBaseSO>();
            actorDataBase.id = GUID.Generate().ToString();
            actorDataBase.name = $"actorDB-{actorDataBase.id}";
            actorDataBase.hideFlags = HideFlags.HideInHierarchy;

            return actorDataBase;
        }

        public static ItemDataBaseSO CreateItemDataBase()
        {
            ItemDataBaseSO itemDataBase = ScriptableObject.CreateInstance<ItemDataBaseSO>();
            itemDataBase.id = GUID.Generate().ToString();
            itemDataBase.name = $"itemDB-{itemDataBase.id}";
            itemDataBase.hideFlags = HideFlags.HideInHierarchy;

            return itemDataBase;
        }
    }
}
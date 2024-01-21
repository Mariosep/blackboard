using Blackboard.Actors;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Blackboard
{
    public static class BlackboardManager
    {
        private static BlackboardSO _blackboardDataBase;

        public static BlackboardSO BlackboardDataBase
        {
            get
            {
                if (_blackboardDataBase == null)
                    LoadBlackboardAsset();

                return _blackboardDataBase;
            }
        }

        public static FactDataBaseSO FactDataBase => BlackboardDataBase.factDataBase;
        public static ActorDataBaseSO ActorDataBase => BlackboardDataBase.actorDataBase;
        public static ItemDataBaseSO ItemDataBase => BlackboardDataBase.itemDataBase;
                
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void LoadBlackboardAsset()
        {
            var handle = Addressables.LoadAssetAsync<BlackboardSO>("Blackboard");
            BlackboardSO blackboard = await handle.Task;

            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                _blackboardDataBase = blackboard;
            else
                Debug.LogError("Failed to load asset: Blackboard");
        }
        
        public static BoolFactSO GetBoolFact(string factName)
        {
            return BlackboardDataBase.GetBoolFactByName(factName);
        }
    
        public static IntFactSO GetIntFact(string factName)
        {
            return BlackboardDataBase.GetIntFactByName(factName);
        }
    
        public static FloatFactSO GetFloatFact(string factName)
        {
            return BlackboardDataBase.GetFloatFactByName(factName);
        }
    
        public static StringFactSO GetStringFact(string factName)
        {
            return BlackboardDataBase.GetStringFactByName(factName);
        }
    
        public static ActorSO GetActor(string actorName)
        {
            return BlackboardDataBase.GetActorByName(actorName);
        }
    
        public static ItemSO GetItem(string itemName)
        {
            return BlackboardDataBase.GetItemByName(itemName);
        }
        
        public static void SaveState()
        {
            BlackboardDataBase.SaveState();
        }
    
        public static void RevertChanges()
        {
            BlackboardDataBase.RevertChanges();
        }
    }
}
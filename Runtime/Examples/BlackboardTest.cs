using Blackboard.Actors;
using Blackboard.Facts;
using Blackboard.Items;
using UnityEngine;

namespace Blackboard.Examples
{
    public class BlackboardTest : MonoBehaviour
    {
        [ShowValue]
        public BoolFactSO boolFact;
        [ShowValue]
        public IntFactSO intFact;
        public FloatFactSO floatFact;
    
        public StringFactSO stringFact;
    
        public ActorSO actor;
        public ItemSO item;

        private void Start()
        {
            if(boolFact != null)
                boolFact.onValueChanged += OnBoolFactValueChanged;
            //intFact.onValueChanged += OnCoinCollected;
        }

        [ContextMenu("ToggleFactValue")]
        private void ToggleFactValue()
        {
            if (boolFact != null)
                boolFact.Value = !boolFact.Value;
        }
    
        [ContextMenu("IncrementFactValue")]
        private void IncrementFactValue()
        {
            /*if (intFact.HasValue)
            intFact.Value += 1;*/
        }

        private void OnBoolFactValueChanged(bool value)
        {
            Debug.Log($"{boolFact.Name} changed to: {value}");
        }
    
        private void OnCoinCollected(int coinsCount)
        {
            if(coinsCount == 10)
                Debug.Log($"Quest completed.");
        }

        private void OnActorEventInvoked(ActorSO actor)
        {
            Debug.Log($"Actor event invoked with {actor.Name}");
        }
    
        private void OnDestroy()
        {
            if(boolFact != null)
                boolFact.onValueChanged -= OnBoolFactValueChanged;
        }
    }
}
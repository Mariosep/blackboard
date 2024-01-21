using Blackboard.Events;
using UnityEngine;

namespace Blackboard.Examples
{
    public class EventTester : MonoBehaviour
    {
        public string itemName;
        public int estaVariableEsMuyLargaParaPoderComprobarComoSeEscribeEnElEditor;

        
        
        private GeneralEventChannel eventChannel;

        private void Start()
        {
            eventChannel = ServiceLocator.Get<GeneralEventChannel>();
            //eventChannel.onScoreIncreased += DebugEventArg;
        }

        private void DebugEventArg(int number)
        {
            Debug.Log(number);
        }
    }
}
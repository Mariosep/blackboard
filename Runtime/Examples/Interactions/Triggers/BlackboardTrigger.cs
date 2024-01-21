using Blackboard.Commands;
using Blackboard.Requirement;
using UnityEngine;

namespace Blackboard.Interactions
{
    public class BlackboardTrigger : MonoBehaviour
    {
        public bool triggerOnce = true;

        public enum EnableOn
        {
            Start,
            TriggerEnter,
            TriggerStay
        }

        public EnableOn enableOn;
        private bool isEnabled;

        public RequirementsSO requirements;

        public CommandList onTrigger;

        private Requirements req;

        private bool triggered;

        private void Awake()
        {
            req = new Requirements(requirements);
        }

        #region EnableOn Logic

        private void Start()
        {
            if (enableOn == EnableOn.Start)
            {
                isEnabled = true;
                req = new Requirements(requirements);
                req.onFulfilled += OnTrigger;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (enableOn == EnableOn.TriggerEnter && !isEnabled && !triggered)
            {
                isEnabled = true;

                if (req.AreFulfilled)
                    OnTrigger();
            }
        }

        /*private void OnTriggerStay(Collider other)
        {
            if (enableOn == EnableOn.TriggerStay && !isEnabled && !triggered)
            {
                isEnabled = true;
                
                
                
                req.onFulfilled += OnTrigger;
            }
        }*/

        private void OnTriggerExit(Collider other)
        {
            if (enableOn is EnableOn.TriggerStay or EnableOn.TriggerEnter && isEnabled && !triggered)
            {
                isEnabled = false;
                req.onFulfilled -= OnTrigger;
            }
        }

        #endregion

        private void OnTrigger()
        {
            onTrigger.Execute();

            if (triggerOnce)
            {
                req.onFulfilled -= OnTrigger;
                triggered = true;
            }
        }

        private void Reset()
        {
            triggerOnce = true;
            enableOn = EnableOn.Start;

            if (requirements != null)
                requirements.Reset();

            if (onTrigger != null)
                onTrigger.Reset();
        }
    }

}
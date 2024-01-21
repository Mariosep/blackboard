using Blackboard.Events;
using QuestDesigner;
using UnityEngine;

namespace Blackboard.Interactions
{
    public class Interactor : MonoBehaviour
    {
        public IInteractable currentInteractableReachable;

        public float maxInteractionDistance = 3f;

        private InputManager inputManager;
        private InteractionEventChannel interactionEventChannel;

        private Transform mainCamera;

        private readonly int interactableLayerMask = 1 << 6;

        private void Start()
        {
            inputManager = ServiceLocator.Get<InputManager>();
            interactionEventChannel = ServiceLocator.Get<InteractionEventChannel>();

            mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, maxInteractionDistance,
                    interactableLayerMask))
            {
                if (hit.collider.gameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    if (interactable != currentInteractableReachable)
                    {
                        if (currentInteractableReachable == null)
                            OnInteractableTriggerExit(currentInteractableReachable);

                        OnInteractableTriggerEnter(interactable);
                    }
                }
            }
            else if (currentInteractableReachable != null)
            {
                OnInteractableTriggerExit(currentInteractableReachable);
            }
        }

        private void OnInteractableTriggerEnter(IInteractable interactable)
        {
            currentInteractableReachable = interactable;
            inputManager.OnInteractPerformed += OnInteractPerformed;

            interactionEventChannel.onInteractableTriggerEnter?.Invoke(interactable);
        }

        private void OnInteractableTriggerExit(IInteractable interactable)
        {
            currentInteractableReachable = null;
            inputManager.OnInteractPerformed -= OnInteractPerformed;

            interactionEventChannel.onInteractableTriggerExit?.Invoke(interactable);
        }

        private void OnInteractPerformed()
        {
            if (currentInteractableReachable.InteractionEnabled && currentInteractableReachable.CanInteract())
            {
                currentInteractableReachable?.Interact();

                interactionEventChannel.onInteract?.Invoke(currentInteractableReachable);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (mainCamera == null)
                mainCamera = Camera.main.transform;

            Gizmos.color = Color.red;

            Gizmos.DrawLine(mainCamera.position, mainCamera.position + mainCamera.forward * maxInteractionDistance);
        }
    }

}
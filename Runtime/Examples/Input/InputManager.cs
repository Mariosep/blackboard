using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuestDesigner
{
    public class InputManager : Controls.IPlayerActions, Controls.IUIActions
    {
        public Vector2 MouseDelta;
        public Vector2 Movement;

        public Action OnInteractPerformed;
        public Action OnJumpPerformed;
        public Action OnOpenQuestJournalPerformed;
        public Action OnRestartPerformed;

        private Controls controls;

        public InputManager()
        {
            if (controls != null)
                return;

            controls = new Controls();
            controls.Player.SetCallbacks(this);
            controls.Player.Enable();
            controls.UI.SetCallbacks(this);
            controls.UI.Enable();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnInteractPerformed?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnJumpPerformed?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnOpenQuestJournal(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnOpenQuestJournalPerformed?.Invoke();
        }

        public void OnRestart(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            OnRestartPerformed?.Invoke();
        }
    }

}
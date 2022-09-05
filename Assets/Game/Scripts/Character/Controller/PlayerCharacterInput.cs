using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Character
{
    [RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
    public class PlayerCharacterInput : MonoBehaviour, ICharacterInput
    {
        [Serializable]
        private class ActionNames
        {
            [SerializeField] public string movement = "Movement";
            [SerializeField] public string looking = "Looking";
            [SerializeField] public string jump = "Jump";
        }

        [SerializeField] private ActionNames actionNames;
        [SerializeField] private UnityEngine.InputSystem.PlayerInput input;

        [Header("Registered Actions")]
        [SerializeField] private InputAction movementAction;
        [SerializeField] private InputAction lookingAction;
        [SerializeField] private InputAction jumpAction;

        private Vector2 _lookInput;
        private Vector2 _movementInput;
        private bool _jumpInput;

        public Vector2 LookInput => _lookInput;
        public Vector2 MovementInput => _movementInput;
        public bool JumpInput
        {
            get
            {
                var value = _jumpInput;
                
                // reset input once it has was retrieved
                _jumpInput = false;
                
                return value;
            }
        }
        
        private void Awake()
        {
            if (!FindActions()) Debug.LogError("Failed to find Player input actions");
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _lookInput = Vector2.zero;
            _movementInput = Vector2.zero;
            _jumpInput = false;
            
            SubscribeMovement(true);
            SubscribeLooking(true);
            SubscribeJumping(true);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            SubscribeMovement(false);
            SubscribeLooking(false);
            SubscribeJumping(false);
        }

        #region subscribtions

        private void SubscribeLooking(bool subscribe)
        {
            if (subscribe)
            {
                lookingAction.performed += LookingActionOnPerformed;
                lookingAction.canceled += LookingActionOnCanceled;
            }
            else
            {
                lookingAction.performed -= LookingActionOnPerformed;
                lookingAction.canceled -= LookingActionOnCanceled;
            }
        }

        private void SubscribeMovement(bool subscribe)
        {
            if (subscribe)
            {
                movementAction.performed += MovementActionOnPerformed;
                movementAction.canceled += MovementActionOnCanceled;
            }
            else
            {
                movementAction.performed -= MovementActionOnPerformed;
                movementAction.canceled -= MovementActionOnCanceled;
            }
        }

        private void SubscribeJumping(bool subscribe)
        {
            if (subscribe)
            {
                jumpAction.performed += JumpActionOnPerformed;
            }
            else
            {
                jumpAction.performed -= JumpActionOnPerformed;
            }
        }

        #endregion

        #region action callbacks

        private void MovementActionOnPerformed(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }

        private void MovementActionOnCanceled(InputAction.CallbackContext context)
        {
            _movementInput = Vector2.zero;
        }

        private void LookingActionOnPerformed(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        private void LookingActionOnCanceled(InputAction.CallbackContext context)
        {
           _lookInput = Vector2.zero;
        }

        private void JumpActionOnPerformed(InputAction.CallbackContext context)
        {
            _jumpInput = true;
        }

        #endregion

        #region private

        private bool FindActions()
        {
            if (!input)
                return false;
            
            movementAction = input.actions.FindAction(actionNames.movement);
            lookingAction = input.actions.FindAction(actionNames.looking);
            jumpAction = input.actions.FindAction(actionNames.jump);

            return movementAction != null
                   && lookingAction != null
                   && jumpAction != null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _editor_this = this;

            if (!input) TryGetComponent(out input);
            if (input)
            {
                input.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;

                if (!FindActions())
                    Debug.LogError("One of the action is missing in the assigned input asset");
            }
        }

        private static PlayerCharacterInput _editor_this;

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void ResolveReferences()
        {
            if (_editor_this && !_editor_this.input.uiInputModule)
            {
                var uiInputModule = FindObjectOfType<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
                if (uiInputModule)
                    _editor_this.input.uiInputModule = uiInputModule;
            }
        }
#endif

        #endregion
    }
}
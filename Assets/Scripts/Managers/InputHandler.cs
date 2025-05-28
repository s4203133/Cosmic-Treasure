using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LMO {

    [CreateAssetMenu(menuName = "Input/Input Handler", fileName = "New Input Handler")]
    public class InputHandler : ScriptableObject {
        [SerializeField] private InputActionAsset inputActions;

        // All possible inputs
        private InputAction move;
        private static InputAction jump;
        private InputAction spin;
        private InputAction groundPound;
        private InputAction select;
        private InputAction grapple;
        private InputAction quit;
        private InputAction pause;

        private InputAction UILeft;
        private InputAction UIRight;
        private InputAction UIUp;
        private InputAction UIDown;

        // Input Delegates
        public delegate void InputEvent();
        public delegate void Vector2Event(Vector2 value);

        // Input Events (Other scripts can subscribe to these to respond when input is recieved)
        public static Vector2Event moveStarted;
        public static Vector2Event movePerformed;
        public static Vector2Event moveCancelled;

        public static InputEvent jumpStarted;
        public static InputEvent jumpPerformed;
        public static InputEvent jumpCancelled;

        public static InputEvent SpinStarted;

        public static InputEvent groundPoundStarted;

        public static InputEvent selectedStarted;

        public static InputEvent grappleStarted;
        public static InputEvent grappleEnded;

        public static InputEvent quitStarted;

        public static InputEvent pauseStarted;

        public static InputEvent Left;
        public static InputEvent Right;
        public static InputEvent Up;
        public static InputEvent Down;
        public static InputEvent click;

        public static Action Enable;
        public static Action Disable;
        private bool playerInputDisabled;

        public static bool jumpBeingPressed => jump.IsPressed();

        private void OnEnable() {
            EnableInputActions();
            InitialiseInputActions();
            SubscribeInputEvents();
        }

        private void OnDisable() {
            UnsubscribeInputEvents();
            DisableInputActions();
        }

        // Invoke events when input is recieved, updated, or cancelled

        private void OnMove(InputAction.CallbackContext context) {
            if(playerInputDisabled) { return; }
            if (context.started) {
                moveStarted?.Invoke(context.ReadValue<Vector2>());
            } else if (context.performed) {
                movePerformed?.Invoke(context.ReadValue<Vector2>());
            } else if (context.canceled) {
                moveCancelled?.Invoke(context.ReadValue<Vector2>());
            }
        }

        private void OnJump(InputAction.CallbackContext context) {
            if (context.started) {
                click?.Invoke();
            }
            if (playerInputDisabled) { return; }
            if (context.started) {
                jumpStarted?.Invoke();
            } else if (context.performed) {
                jumpPerformed?.Invoke();
            } else if (context.canceled) {
                jumpCancelled?.Invoke();
            }
        }

        private void OnSpin(InputAction.CallbackContext context) {
            if (playerInputDisabled) { return; }
            if (context.started) {
                SpinStarted?.Invoke();
            }
        }

        private void OnGroundPound(InputAction.CallbackContext context) {
            if (playerInputDisabled) { return; }
            if (context.started) {
                groundPoundStarted?.Invoke();
            }
        }

        private void OnSelect(InputAction.CallbackContext context) {
            if (playerInputDisabled) { return; }
            if (context.started) {
                selectedStarted?.Invoke();
            }
        }

        private void OnGrapple(InputAction.CallbackContext context) {
            if (playerInputDisabled) { return; }
            if (context.started) {
                grappleStarted?.Invoke();
            } else if (context.canceled) {
                grappleEnded?.Invoke();
            }
        }

        private void OnQuit(InputAction.CallbackContext context) {
            if (playerInputDisabled) { return; }
            if (context.started) {
                quitStarted?.Invoke();
            }
        }

        //<NR>
        private void OnPause(InputAction.CallbackContext context) {
            if (playerInputDisabled) { return; }
            if (context.started) {
                pauseStarted?.Invoke();
            }
        }
        //</NR>

        private void OnLeft(InputAction.CallbackContext context) {
            if (context.started) {
                Left?.Invoke();
            }
        }

        private void OnRight(InputAction.CallbackContext context) {
            if (context.started) {
                Right?.Invoke();
            }
        }

        private void OnUp(InputAction.CallbackContext context) {
            if (context.started) {
                Up?.Invoke();
            }
        }

        private void OnDown(InputAction.CallbackContext context) {
            if (context.started) {
                Down?.Invoke();
            }
        }

        private void InitialiseInputActions() {
            move = inputActions.FindAction("Move");
            jump = inputActions.FindAction("Jump");
            spin = inputActions.FindAction("Spin");
            groundPound = inputActions.FindAction("GroundPound");
            select = inputActions.FindAction("Select");
            grapple = inputActions.FindAction("Grapple");
            quit = inputActions.FindAction("Quit");
            pause = inputActions.FindAction("Pause");
            UILeft = inputActions.FindAction("UILeft");
            UIRight = inputActions.FindAction("UIRight");
            UIUp = inputActions.FindAction("UIUp");
            UIDown = inputActions.FindAction("UIDown");
        }

        private void SubscribeMoveEvents() {
            move.started += OnMove;
            move.performed += OnMove;
            move.canceled += OnMove;
        }

        private void UnsubscribeMoveEvents() {
            move.started -= OnMove;
            move.performed -= OnMove;
            move.canceled -= OnMove;
        }

        private void SubscribeJumpEvents() {
            jump.started += OnJump;
            jump.performed += OnJump;
            jump.canceled += OnJump;
        }

        private void UnsubscribeJumpEvents() {
            jump.started -= OnJump;
            jump.performed -= OnJump;
            jump.canceled -= OnJump;
        }

        private void SubscribeSpinEvents() {
            spin.started += OnSpin;
        }

        private void UnsubscribeSpinEvents() {
            spin.started -= OnSpin;
        }

        private void SubscribeGroundPoundEvents() {
            groundPound.started += OnGroundPound;
        }

        private void UnsubscribeGroundPoundEvents() {
            groundPound.started -= OnGroundPound;
        }

        private void SubscribeSelectEvents() {
            select.started += OnSelect;
        }

        private void UnsubscribeSelectEvents() {
            select.started -= OnSelect;
        }        
        
        private void SubscribeGrappleEvents() {
            grapple.started += OnGrapple;
            grapple.canceled += OnGrapple;
        }

        private void UnsubscribeGrappleEvents() {
            grapple.started -= OnGrapple;
        }

        private void SubscribeQuitEvents() {
            quit.started += OnQuit;
        }

        private void UnsubscribeQuitEvents() {
            quit.started -= OnQuit;
        }

        //<NR>
        private void SubscribePauseEvents() {
            pause.started += OnPause;
        }

        private void UnsubscribePauseEvents() {
            pause.started -= OnPause;
        }
        //</NR>

        private void SubscribeUIEvents() {
            UILeft.started += OnLeft;
            UIRight.started += OnRight;
            UIUp.started += OnUp;
            UIDown.started += OnDown;
        }

        private void UnsubscribeUIEvents() {
            UILeft.started -= OnLeft;
            UIRight.started -= OnRight;
            UIUp.started -= OnUp;
            UIDown.started -= OnDown;
        }

        private void SubscribeInputEvents() {
            SubscribeMoveEvents();
            SubscribeJumpEvents();
            SubscribeSpinEvents();
            SubscribeGroundPoundEvents();
            SubscribeSelectEvents();
            SubscribeGrappleEvents();
            SubscribeQuitEvents();
            SubscribePauseEvents();
            SubscribeUIEvents();
            Enable += EnableInput;
            Disable += DisableInput;
        }

        private void UnsubscribeInputEvents() {
            UnsubscribeMoveEvents();
            UnsubscribeJumpEvents();
            UnsubscribeSpinEvents();
            UnsubscribeGroundPoundEvents();
            UnsubscribeSelectEvents();
            UnsubscribeGrappleEvents();
            UnsubscribeQuitEvents();
            UnsubscribePauseEvents();
            UnsubscribeUIEvents();
            Enable -= EnableInput;
            Disable -= DisableInput;
        }

        private void EnableInputActions() {
            inputActions.Enable();
            move?.Enable();
            jump?.Enable();
            spin?.Enable();
            groundPound?.Enable();
            select?.Enable();
            grapple?.Enable();
            quit?.Enable();
            pause?.Enable();
            UILeft?.Enable();
            UIRight?.Enable();
            UIUp?.Enable();
            UIDown?.Enable();
        }

        private void DisableInputActions() {
            move?.Disable();
            jump?.Disable();
            spin?.Disable();
            groundPound?.Disable();
            select?.Disable();
            grapple?.Disable();
            quit?.Disable();
            pause?.Disable();
            inputActions?.Disable();
            UILeft?.Disable();
            UIRight?.Disable();
            UIUp?.Disable();
            UIDown?.Disable();
        }

        private void EnableInput() {
            playerInputDisabled = false;
        }

        private void DisableInput() {
            playerInputDisabled = true;
        }
    }
}
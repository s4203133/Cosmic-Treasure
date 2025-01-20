using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="Input/Input Handler", fileName = "New Input Handler")]
public class InputHandler : ScriptableObject
{
    [SerializeField] private InputActionAsset inputActions;

    private InputAction move;
    private InputAction jump;

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

    private void OnEnable() {
        EnableInputActions();
        InitialiseInputActions();
        SubscribeInputEvents();
    }

    private void OnDisable() {
        UnsubscribeInputEvents();
        DisableInputActions();
    }

    private void OnMove(InputAction.CallbackContext context) {
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
            jumpStarted?.Invoke();
        } else if (context.performed) {
            jumpPerformed?.Invoke();
        } else if (context.canceled) {
            jumpCancelled?.Invoke();
        }
    }

    private void InitialiseInputActions() {
        move = inputActions.FindAction("Move");
        jump = inputActions.FindAction("Jump");
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

    private void SubscribeInputEvents() {
        SubscribeMoveEvents();
        SubscribeJumpEvents();
    }

    private void UnsubscribeInputEvents() {
        UnsubscribeMoveEvents();
        UnsubscribeJumpEvents();
    }

    private void EnableInputActions() {
        inputActions.Enable();
        move?.Enable();
        jump?.Enable();
    }

    private void DisableInputActions() {
        inputActions.Disable();
        move.Disable(); 
        jump.Disable();
    }
}

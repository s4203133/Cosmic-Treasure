using UnityEngine;

public class PlayerHoverMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float maxForce;
    [SerializeField] private float forceIncrease;
    private float force;

    private Vector3 velocity;
    private Vector3 moveDirection;

    private Vector2 moveInput;

    public void HandleMovement() {
        GetMoveInput();
        IncreaseVelocity();
    }

    private void GetMoveInput() {
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void IncreaseVelocity() {
        velocity = moveDirection * forceIncrease;
        rigidBody.AddForce(velocity * Time.deltaTime);
    }

    private void OnEnable() {
        SubscribeMoveEvents();
    }

    private void OnDisable() {
        UnsubscribeMoveEvents();
    }

    // Subscribing functions to input events

    private void StartMovement(Vector2 value) {
        moveInput = value;
    }

    private void PerformMovement(Vector2 value) {
        moveInput = value;
    }

    private void StopMovement(Vector2 value) {
        moveInput = Vector2.zero;
    }

    public void SubscribeMoveEvents() {
        InputHandler.moveStarted += StartMovement;
        InputHandler.movePerformed += PerformMovement;
        InputHandler.moveCancelled += StopMovement;
    }

    public void UnsubscribeMoveEvents() {
        InputHandler.moveStarted -= StartMovement;
        InputHandler.movePerformed -= PerformMovement;
        InputHandler.moveCancelled -= StopMovement;
    }
}

using UnityEngine;

public class Grounded : MonoBehaviour
{
    private bool isGrounded;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask jumpLayers;
    [SerializeField] private float groundCheckRadius;

    private bool hasStartedJump;
    private bool sentGroundedEvent;

    public delegate void CustomEvent();
    public static CustomEvent OnLanded;

    public bool IsOnGround => isGrounded;

    void Update()
    {
        CheckIsGrounded();
        EndStartOfJump();
        SendGroundedEvent();
    }

    private bool DetectingGround() {
        return Physics.CheckSphere(groundPoint.position, groundCheckRadius, jumpLayers);
    }

    private void CheckIsGrounded() {
        if(hasStartedJump) { 
            isGrounded = false; 
            return; 
        }
        isGrounded = DetectingGround();
    }

    private void EndStartOfJump() {
        if(!DetectingGround()) {
            hasStartedJump = false;
        }
    }

    public void NotifyLeftGround() {
        hasStartedJump = true;
    }

    private void SendGroundedEvent() {
        if(isGrounded) {
            if (!sentGroundedEvent) {
                OnLanded?.Invoke();
                sentGroundedEvent = true;
            }
        } else {
            sentGroundedEvent = false;
        }
    }
}

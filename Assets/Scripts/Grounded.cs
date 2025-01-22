using UnityEngine;

public class Grounded : MonoBehaviour
{
    private bool isGrounded;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask jumpLayers;
    [SerializeField] private float groundCheckRadius;

    private bool hasStartedJump;

    void Update()
    {
        CheckIsGrounded();
        foo();
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

    private void foo() {
        if(!DetectingGround()) {
            hasStartedJump = false;
        }
    }

    public bool IsOnGround() {
        return isGrounded;
    }

    public void NotifyLeftGround() {
        hasStartedJump = true;
    }
}

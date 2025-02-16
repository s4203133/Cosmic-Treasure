using LMO;
using UnityEngine;

public class LeanPlayer : MonoBehaviour
{
    [SerializeField] private float leanAmount;
    [SerializeField] private float smoothness;
    [SerializeField] private PlayerInput input;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform playerVisualTransform;
    private CameraDirection cameraDirection;
    private Transform thisTransform;

    Vector3 direction;

    void Start() {
        cameraDirection = new CameraDirection(cameraTransform);
        thisTransform = transform;
    }

    void Update()
    {
        GetLeanDirection();
    }

    private void GetLeanDirection() {
        GetMoveDirection();
        //Vector3 leanDirection = (-direction * leanAmount);
        //Vector3 newDirection = Vector3.Slerp(playerVisualTransform.forward, leanDirection, smoothness);
        //leanDirection.y = thisTransform.position.y + 0.75f;
        //thisTransform.LookAt(playerVisualTransform.position + newDirection);
        //Quaternion lookDirection = Quaternion.LookRotation(moveDirection, thisTransform.up);
        //thisTransform.rotation = Quaternion.RotateTowards(thisTransform.rotation, lookDirection, smoothness);
        //thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, lookDirection, smoothness);
    }

    private void GetMoveDirection() {
        if (input.moveInput == Vector2.zero) {
            thisTransform.rotation = Quaternion.Lerp(thisTransform.localRotation, Quaternion.Euler(0, thisTransform.localRotation.y, thisTransform.localRotation.z), smoothness);
        } else {
            thisTransform.rotation = Quaternion.Lerp(thisTransform.localRotation, Quaternion.Euler(leanAmount, thisTransform.localRotation.y, thisTransform.localRotation.z), smoothness);
        }

        //cameraDirection.CalculateDirection();
        //Vector3 forwardMovement = cameraDirection.Forward * input.moveInput.y;
        //Vector3 rightMovement = cameraDirection.Right * input.moveInput.x;
        //direction =  forwardMovement + rightMovement;
    }
}

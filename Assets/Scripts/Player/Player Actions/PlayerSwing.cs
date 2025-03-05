using UnityEngine;

namespace LMO {

    public class PlayerSwing : MonoBehaviour {

        [Header("CONNECTING SETTINGS")]
        [SerializeField] private float duration;
        private float timer;
        private bool connected;

        [Header("MOVEMENT SETTINGS")]
        [SerializeField] private PlayerMovementSettings movementSettings;
        public PlayerMovementSettings MovementSettings => movementSettings;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private float swingSpeed;
        private Vector3 moveDirection;

        [SerializeField] private float rotateSpeed;
        private RotateCharacter rotation;
        private Vector3 rotateDirection;

        [Header("JOINT SETTINGS")]
        [SerializeField] private SwingJointSettings jointSettings;
        private Vector3 jointPosition;

        [Header("COMPONENTS")]
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Rigidbody rigidBody;

        [Header("CAMERA")]
        [SerializeField] private Transform camTransform;
        private CameraDirection camDirection;

        private bool landedFromSwing;
        public bool Landed => landedFromSwing;

        private void Awake() {
            rotation = new RotateCharacter(playerTransform);
            camDirection = new CameraDirection(camTransform);
        }

        public void StartSwing(Vector3 swingPoint) {
            jointPosition = swingPoint;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            timer = duration;
        }

        public void CountdownConnectionTimer() {
            if (connected) {
                return;
            }
            timer -= Time.deltaTime;
            rigidBody.velocity = Vector3.zero;
            if (timer <= 0) {
                InitialiseSwinging();
            }
        }

        private void InitialiseSwinging() {
            landedFromSwing = false;
            rigidBody.useGravity = true;
            jointSettings.InitialiseJoint(playerTransform, jointPosition);
            connected = true;
        }

        public void PerformSwing() {
            if (!connected) { 
                return; 
            }
            
            camDirection.CalculateDirection();
            GetRotateDirection();
            rotation.RotateTowardsDirection(rotateDirection, rotateSpeed);

            GetMoveDirection();
            rigidBody.AddForce(moveDirection * swingSpeed * Time.fixedDeltaTime);
        }

        public void EndSwing() {
            connected = false;
            Destroy(jointSettings.Joint);
        }

        private void GetRotateDirection() {
            Vector3 forwardMovement = camDirection.Forward * Mathf.Abs(playerInput.moveInput.y);
            rotateDirection = forwardMovement;
        }

        private void GetMoveDirection() {
            Vector3 forwardMovement = camDirection.Forward * playerInput.moveInput.y;
            Vector3 rightMovement = camDirection.Right * playerInput.moveInput.x;
            moveDirection = forwardMovement + rightMovement;
        }

        public void RegisterLanded() {
            landedFromSwing = true;
        }
    }
}

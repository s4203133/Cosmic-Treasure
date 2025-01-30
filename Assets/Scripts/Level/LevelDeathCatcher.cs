using UnityEngine;

public class LevelDeathCatcher : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPosition;
    [SerializeField] private CameraControllerBasic cameraController;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            other.gameObject.transform.position = respawnPosition;
            other.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            cameraController.QuickSnapToTarget();
            PlayerStateMachine stateMachine = other.gameObject.GetComponent<PlayerStateMachine>();
            stateMachine.ChangeState(stateMachine.fallingState);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}

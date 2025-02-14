using UnityEngine;

public class SwingManager : MonoBehaviour {

    [SerializeField] private DetectSwingJoints swingJointDetector;
    private bool canSwing;
    public bool CanSwing => canSwing;

    private GameObject swingTarget;
    public GameObject SwingTarget => swingTarget;

    //[SerializeField] private SwingPointUI swingPointUI;

    void Start() {
        swingJointDetector.Initialise();
    }

    private void OnEnable() {
        swingJointDetector.OnSwingPointFound += EnableSwing;
        swingJointDetector.OnSwingPointOutOfRange += DisableSwing;
    }

    private void OnDisable() {
        swingJointDetector.OnSwingPointFound -= EnableSwing;
        swingJointDetector.OnSwingPointOutOfRange -= DisableSwing;
    }

    void Update() {
        swingJointDetector.GetClosestJoint();
    }

    private void EnableSwing(GameObject target) {
        swingTarget = target;
        canSwing = true;
    }

    private void DisableSwing(GameObject target) {
        swingTarget = null;
        canSwing = false;
    }
}

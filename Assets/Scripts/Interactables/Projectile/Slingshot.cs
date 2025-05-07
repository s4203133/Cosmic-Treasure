using UnityEngine;

namespace NR {
    public class Slingshot : MonoBehaviour {
        [SerializeField] private SlingshotJoint joint;
        [SerializeField] private Transform slingshotOrigin;
        [SerializeField] private Transform slingshotLaunch;
        [SerializeField] private GameObject projectile;

        [SerializeField] private float forceMultiplier = 1;
        [SerializeField] private LineRenderer lineRenderer;

        [SerializeField] private int trajectoryPoints = 10;
        
        private void Start() {
            lineRenderer.positionCount = trajectoryPoints;
            if (joint != null) {
                joint.SetParentTransform(slingshotOrigin);
                joint.SlingshotUpdate += PullUpdate;
                joint.SlingshotReleased += ResetLine;
            }
            
            if (ProjectileParent.Instance == null) {
                Debug.LogError("Please add a ProjectileParent object to the scene in order to use slingshots.");
            }
        }

        private void PullUpdate() {
            Vector3 rotateTarget = joint.PlayerDragPosition - transform.position;
            rotateTarget.y = 0;
            transform.right = rotateTarget;  

            float startVelocity = (transform.position - joint.transform.position).sqrMagnitude * forceMultiplier;
            lineRenderer.SetPositions(TrajectoryCalculator.CalculateTrajectoryPath(slingshotLaunch.position, slingshotLaunch.forward, startVelocity, trajectoryPoints, 0.9f));
        }

        private void ResetLine() {
            lineRenderer.SetPositions(new Vector3[trajectoryPoints]);
        }

        //assigned in-editor to the Interact Actions event
        public void LaunchSlingshot() {
            ResetLine();
            float distance = (transform.position - joint.transform.position).sqrMagnitude;

            ProjectileParent.Instance.SpawnProjectile(slingshotLaunch, distance * forceMultiplier);
        }
    }
}
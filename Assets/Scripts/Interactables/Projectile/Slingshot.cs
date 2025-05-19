using UnityEngine;

namespace NR {
    /// <summary>
    /// Main behaviour script for slingshots.
    /// The components needed to input are set up in the prefab.
    /// When the SlingshotJoint is pulled back, it displays trajectory and rotates. When it is released, it fires a projectile.
    /// </summary>
    public class Slingshot : MonoBehaviour {
        [SerializeField] private SlingshotJoint joint;
        [SerializeField] private Transform slingshotOrigin;
        [SerializeField] private Transform slingshotLaunch;
        [SerializeField] private GameObject projectile;

        [SerializeField] private float forceMultiplier = 1;
        [SerializeField] private float lineLength = 0.9f;
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

        // Called by event every frame while the slingshot joint is being grappled.
        private void PullUpdate() {
            Vector3 rotateTarget = joint.PlayerDragPosition - transform.position;
            rotateTarget.y = 0;
            transform.right = rotateTarget;  

            float startVelocity = (transform.position - joint.transform.position).sqrMagnitude * forceMultiplier;
            lineRenderer.SetPositions(TrajectoryCalculator.CalculateTrajectoryPath(slingshotLaunch.position, slingshotLaunch.forward, startVelocity, trajectoryPoints, lineLength));
        }

        private void ResetLine() {
            lineRenderer.SetPositions(new Vector3[trajectoryPoints]);
        }

        // Assigned in-editor to the Interact Actions event
        public void LaunchSlingshot() {
            ResetLine();
            float distance = (transform.position - joint.transform.position).sqrMagnitude;

            ProjectileParent.Instance.SpawnProjectile(slingshotLaunch, distance * forceMultiplier);
        }
    }
}
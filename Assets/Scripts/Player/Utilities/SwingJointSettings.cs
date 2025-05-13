using UnityEngine;
using NR;

namespace LMO {

    [System.Serializable]
    public class SwingJointSettings {
        private SpringJoint currentJoint;
        public SpringJoint Joint => currentJoint;

        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        public float MaxDistance => maxDistance;

        [SerializeField] private float spring;
        [SerializeField] private float damper;
        [SerializeField] private float massScale;

        public void InitialiseJoint(Transform player, Vector3 connectPoint) {
            currentJoint = player.gameObject.AddComponent<SpringJoint>();
            currentJoint.autoConfigureConnectedAnchor = false;
            currentJoint.connectedAnchor = connectPoint;
            currentJoint.anchor = new Vector3(0, 1, 0);

            currentJoint.minDistance = minDistance;
            currentJoint.maxDistance = maxDistance;

            currentJoint.spring = spring;
            currentJoint.damper = damper;
            currentJoint.massScale = massScale;
        }

        public void InitialiseJoint(Transform player, Vector3 connectPoint, GrapplePoint grapplePoint) {
            //<NR>
            float grappleMaxDistance = maxDistance;
            if (grappleIsSwing) {
                grappleMaxDistance = grapplePoint.DetectionRange - 2;
            } else if (grappleIsSlingShot) {
                grappleMaxDistance = grapplePoint.DetectionRange + 5;
            }
            //</NR>



            target = grapplePoint;
            if (grappleIsSwing) {
                Debug.Log("Swing Joint");
            } else if (grappleIsSlingShot) {
                Debug.Log("Sling Shot Joint");
            }



            currentJoint = player.gameObject.AddComponent<SpringJoint>();
            currentJoint.autoConfigureConnectedAnchor = false;
            currentJoint.connectedAnchor = connectPoint;
            currentJoint.anchor = new Vector3(0, 1, 0);

            currentJoint.minDistance = minDistance;
            currentJoint.maxDistance = grappleMaxDistance;

            currentJoint.spring = spring;
            currentJoint.damper = damper;
            currentJoint.massScale = massScale;
        }

        private GrapplePoint target;
        private SwingJoint grappleIsSwing => target as SwingJoint;
        private SlingshotJoint grappleIsSlingShot => target as SlingshotJoint;
    }
}
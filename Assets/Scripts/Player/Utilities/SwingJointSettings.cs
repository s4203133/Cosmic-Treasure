using UnityEngine;

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
    }
}
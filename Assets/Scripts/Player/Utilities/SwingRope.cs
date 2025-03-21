using UnityEngine;

namespace LMO {

    public class SwingRope : MonoBehaviour {
        private LineRenderer lineRenderer;

        [SerializeField] private Transform ropeStartPoint;
        private Vector3 ropeEndPoint;
        private Vector3 currentGrapplePosition;

        [SerializeField] private int segments;
        [SerializeField] private float speed;
        [SerializeField] private float waveCount;
        [SerializeField] private float waveHeight;
        [SerializeField] private AnimationCurve ropeShape;

        private Spring spring;
        [SerializeField] private float damping;
        [SerializeField] private float strength;
        [SerializeField] private float velocity;

        private bool isShooting;

        void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = segments + 1;
            spring = new Spring();
            spring.SetTarget(0);
        }

        private void LateUpdate() {
            DrawRope();
        }

        private void DrawRope() {
            if (!isShooting) {
                return;
            }

            spring.SetDamper(damping);
            spring.SetStrength(strength);
            spring.Update(TimeValues.Delta);

            Vector3 startPos = ropeStartPoint.position;
            Vector3 endPos = ropeEndPoint;
            Vector3 up = Quaternion.LookRotation(startPos - endPos).normalized * Vector3.up;

            currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, endPos, speed);

            for (int i = 0; i < segments + 1; i++) {
                float delta = i / (float)segments;
                Vector3 offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI * spring.Value * ropeShape.Evaluate(delta));
                lineRenderer.SetPosition(i, Vector3.Lerp(startPos, currentGrapplePosition, delta) + offset);
            }
        }

        public void SetRopeTarget(Transform target) {
            ropeEndPoint = target.position;
            currentGrapplePosition = ropeStartPoint.position;
            lineRenderer.positionCount = segments + 1;
            isShooting = true;
            spring.SetVelocity(velocity);
        }

        public void DetatchRope() {
            isShooting = false;
            lineRenderer.positionCount = 0;
            spring.Reset();
        }
    }
}

namespace LMO {

    public class Spring {
        private float strength;
        private float damper;
        private float target;
        private float velocity;
        private float value;

        public void Update(float deltaTime) {
            var direction = target - value >= 0 ? 1f : -1f;
            var force = Mathf.Abs(target - value) * strength;
            velocity += (force * direction - velocity * damper) * deltaTime;
            value += velocity * deltaTime;
        }

        public void Reset() {
            velocity = 0f;
            value = 0f;
        }

        public void SetValue(float value) {
            this.value = value;
        }

        public void SetTarget(float target) {
            this.target = target;
        }

        public void SetDamper(float damper) {
            this.damper = damper;
        }

        public void SetStrength(float strength) {
            this.strength = strength;
        }

        public void SetVelocity(float velocity) {
            this.velocity = velocity;
        }

        public float Value => value;
    }
}
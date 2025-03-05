using UnityEngine;

namespace LMO {
    public class CalculateMoveVelocity {

        private Transform _transform;

        public CalculateMoveVelocity(Transform targetTransform) {
            _transform = targetTransform;
        }

        public float CalculateVelocityDifference(Vector3 direction, float threshold) {
            // Compare the direction the player is currently moving in, and the direction they're trying to move to
            float returnValue = Vector3.SqrMagnitude((_transform.position + (direction * 10)) - (_transform.position + (_transform.forward * 10)));
            return (Mathf.Max(1, returnValue * threshold));
        }

        public float CalculateChangeInSpeed(MotionCurve speedChange, float maxSpeed) {
            return (speedChange.CalculateValue(maxSpeed));
        }

        public Vector3 CalculateVelocity(bool canChangeDirectionQuickly, Vector3 direction, float speed, float velocityDifference) {
            // If the player can't make quick changes in direction, only allow them to move in the direction they're facing
            Vector3 returnValue;
            if (!canChangeDirectionQuickly) {
                returnValue = _transform.forward * speed * Time.deltaTime;
            } else {
                // If the player is making a large change in direction, make them move directly towards their target direction,
                // otherwise move in the direction they are facing
                if (velocityDifference > 1) {
                    returnValue = direction * speed * Time.deltaTime;
                } else {
                    returnValue = _transform.forward * speed * Time.deltaTime;
                }
            }
            return returnValue;
        }
    }
}
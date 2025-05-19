using UnityEngine;

namespace NR {
    /// <summary>
    /// Function library for calculations related to projectiles.
    /// </summary>
    public static class TrajectoryCalculator {
        //Adapted from materials provided by University of Gloucestershire
        //Computer Games Programming - CT4101 Programming and Mathematics - Projectile Prediction
        /// <summary>
        /// Calculates the trajectory of a projectile, returning the positions in an array.
        /// </summary>
        /// <param name="origin">Initial position and rotation of the projectile.</param>
        /// <param name="force">Power multiplier of the projectile.</param>
        /// <param name="pathResolution">Number of points to return.</param>
        /// <param name="distance">Amount of the curve to show as a percent (0-1).</param>
        /// <returns>List of positions along the line, at specified resolution and distance.</returns>
        public static Vector3[] CalculateTrajectoryPath(Vector3 origin, Vector3 direction, float force, int pathResolution, float distance = 1) {
            Vector3 initialVelocity = direction * force;

            float finalYVelocity = initialVelocity.y * (1 - distance);
            float airTime = 2 * (finalYVelocity - initialVelocity.y) / Physics.gravity.y;

            float xDisplacement = airTime * initialVelocity.x;

            Vector3[] pathPoints = new Vector3[pathResolution];

            for (int i = 0; i < pathResolution; i++) {
                float time = (i / (float)pathResolution) * airTime;
                Vector3 displacement = initialVelocity * time + Physics.gravity * time * time * 0.5f;
                pathPoints[i] = origin + displacement;
            }

            return pathPoints;
        }

        //Adapted from https://www.forrestthewoods.com/blog/solving_ballistic_trajectories/
        /// <summary>
        /// Caculates and returns the angle a projectile should be launched at to hit a specified target.
        /// </summary>
        /// <param name="origin">Initial position of the projectile.</param>
        /// <param name="targetPos">Desired position for the projectile to shoot towards.</param>
        /// <param name="speed">Scalar speed of the projectile.</param>
        /// <param name="shootHigh">Type of arc (true returns a higher angle)</param>
        /// <returns>Angle of launch, as Vector3.</returns>
        public static Vector3 CalculateLaunchVelocity(Vector3 origin, Vector3 targetPos, float speed, bool shootHigh = false) {
            Vector3 difference = targetPos - origin;
            Vector3 diffXZ = new Vector3(difference.x, 0f, difference.z);

            float x = diffXZ.magnitude;
            float y = difference.y;
            float g = -Physics.gravity.y;
            float s = speed;

            float num1 = (g * x * x) + (2 * s * s * y);
            float num2 = (s * s * s * s) - (g * num1);

            if (num2 < 0) {
                return new Vector3();
            }

            float angle;
            if (shootHigh) {
                angle = Mathf.Atan2(s * s + Mathf.Sqrt(num2), g * x);
            } else {
                angle = Mathf.Atan2(s * s - Mathf.Sqrt(num2), g * x);
            }

            Vector3 returnAngle = diffXZ.normalized * Mathf.Cos(angle) * speed + Vector3.up * Mathf.Sin(angle) * speed;

            return returnAngle;
        }
    }

}


using UnityEngine;

//Adapted from materials provided by University of Gloucestershire
//Computer Games Programming - CT4101 Programming and Mathematics - Projectile Prediction
namespace NR {
    public static class TrajectoryCalculator {
        /// <summary>
        /// Calculates the trajectory of a projectile, returning the positions in an array.
        /// </summary>
        /// <param name="origin">Initial position and rotation of the projectile.</param>
        /// <param name="force">Power multiplier of the projectile.</param>
        /// <param name="pathResolution">Number of points to return.</param>
        /// <param name="distance">Amount of the curve to show as a percent (0-1).</param>
        /// <returns></returns>
        public static Vector3[] CalculateTrajectory(Vector3 origin, Vector3 direction, float force, int pathResolution, float distance = 1) {
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
    }

}


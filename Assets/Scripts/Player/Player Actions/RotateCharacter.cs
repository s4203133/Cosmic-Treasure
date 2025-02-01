using UnityEngine;

public class RotateCharacter {

    private Transform _transform;

    public RotateCharacter(Transform targetTransform) {
        _transform = targetTransform;
    }

    // Smoothly rotate the character towards a given direction, with a rotation speed
    public void RotateTowardsDirection(Vector3 direction, float speed) {
        if (direction != Vector3.zero) {
            Quaternion moveRotation = Quaternion.LookRotation(direction, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, moveRotation, speed * Time.fixedDeltaTime);
        }
    }
}
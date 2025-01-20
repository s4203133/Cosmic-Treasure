using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rigidBody;
    public Vector2 moveDirection;

    public void MoveCharacter() {
        rigidBody.velocity = new Vector3(moveDirection.x * moveSpeed * Time.fixedDeltaTime, rigidBody.velocity.y, moveDirection.y * moveSpeed * Time.fixedDeltaTime);
    }

    public void StopMovement() {
        rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
    }
}

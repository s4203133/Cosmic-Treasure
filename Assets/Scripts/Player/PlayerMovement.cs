using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    public Vector2 moveDirection;

    private void FixedUpdate() {
        rb.velocity = new Vector3(moveDirection.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, moveDirection.y * moveSpeed * Time.fixedDeltaTime);
    }
}

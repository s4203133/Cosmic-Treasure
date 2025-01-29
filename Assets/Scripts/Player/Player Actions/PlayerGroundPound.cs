using System;
using System.Collections;
using UnityEngine;

public class PlayerGroundPound : MonoBehaviour
{
    [Header("GROUND POUND COLLIDERS")]
    [SerializeField] private Collider groundPoundCollider;
    [SerializeField] private Collider groundPoundLandCollider;

    [Header("GROUND POUND SETTINGS")]
    [SerializeField] private float buildUpDuration;
    private bool starting;

    [SerializeField] private float groundPoundSpeed;
    private bool isGroundPounding;

    [SerializeField] private float landDuration;
    [HideInInspector] public bool landed;
    [HideInInspector] public bool finishedGroundPound;

    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Grounded grounded;

    public Action OnGroundPoundStarted;
    public Action OnGroundPoundLanded;

    public void StartGroundPound() {
        OnGroundPoundStarted?.Invoke();

        DisableVelocity();
        InitialiseGroundPound();
        StartCoroutine(BeginGroundPounding());
    }

    public void ApplyGroundPoundForce() {
        HandleGroundPoundBuildUp();
        HandleGroundPoundFalling();
    }

    private void CheckLanded() {
        if (landed) {
            return;
        }

        if(grounded.IsOnGround || rigidBody.velocity == Vector3.zero) {
            StartCoroutine(EndGroundPounding());
        }
    }

    private IEnumerator BeginGroundPounding() {
        // Wait a short delay, then end starting phase, re-enable physics, and activate thr ground pound movement
        yield return new WaitForSeconds(buildUpDuration);
        starting = false;
        rigidBody.useGravity = true;
        isGroundPounding = true;
        groundPoundCollider.enabled = true;
    }

    private IEnumerator EndGroundPounding() {
        // Notify the player has landed and disable the collider
        OnGroundPoundLanded?.Invoke();
        landed = true;
        groundPoundCollider.enabled = false;
        groundPoundLandCollider.enabled = true;
        yield return new WaitForSeconds(landDuration);
        // Finish movement and reset variables
        landed = false;
        finishedGroundPound = true;
        isGroundPounding = false;
        groundPoundLandCollider.enabled = false;
    }

    private void DisableVelocity() {
        rigidBody.velocity = Vector3.zero;
        rigidBody.useGravity = false;
    }

    private void InitialiseGroundPound() {
        starting = true;
        finishedGroundPound = false;
    }

    private void HandleGroundPoundBuildUp() {
        // While the ground pound is starting, remove all velocity from the player
        if (starting) {
            rigidBody.velocity = Vector3.zero;
        }
    }

    private void HandleGroundPoundFalling() {
        // While the ground pound is performing, apply a downwards force to the player, and check if they've reached the ground
        if (isGroundPounding) {
            rigidBody.velocity = new Vector3(0, -groundPoundSpeed, 0);
            CheckLanded();
        }
    }
}

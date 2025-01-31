using System;
using System.Collections;
using UnityEngine;

public class PlayerGroundPound : MonoBehaviour
{
    public bool canGroundPound => !grounded.IsOnGround;

    [Header("GROUND POUND COLLIDERS")]
    [SerializeField] private Collider groundPoundCollider;
    [SerializeField] private Collider groundPoundLandCollider;

    [Header("GROUND POUND SETTINGS")]
    [SerializeField] private float buildUpDuration;
    private bool starting;
    private Coroutine start;
    // How long of the action needs to run before allowing the player to transition to another move
    [SerializeField] private float timeUntilMoveCanBeOverwritten;
    [HideInInspector] public bool canInitiateDifferentAction;

    [SerializeField] private float groundPoundSpeed;
    private bool isGroundPounding;
    public bool PerformingGroundPound => isGroundPounding;

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
        canInitiateDifferentAction = false;
        start = StartCoroutine(BeginGroundPounding());
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
        // Wait a short delay, then end starting phase, re-enable physics, and activate the ground pound movement
        yield return new WaitForSeconds(timeUntilMoveCanBeOverwritten);
        canInitiateDifferentAction = true;
        yield return new WaitForSeconds(buildUpDuration - timeUntilMoveCanBeOverwritten);
        EndGroundPoundBuildUp();
    }

    private void EndGroundPoundBuildUp() {
        starting = false;
        rigidBody.useGravity = true;
        isGroundPounding = true;
        groundPoundCollider.enabled = true;
    }

    private IEnumerator EndGroundPounding() {
        // Notify the player has landed and disable the collider
        OnGroundPoundLanded?.Invoke();
        canInitiateDifferentAction = false;
        landed = true;
        groundPoundCollider.enabled = false;
        groundPoundLandCollider.enabled = true;
        yield return new WaitForSeconds(landDuration);
        // Finish movement and reset variables
        FinishGroundPound();
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

    public void FinishGroundPound() {
        if(finishedGroundPound) {
            return;
        }
        landed = false;
        finishedGroundPound = true;
        isGroundPounding = false;
        groundPoundCollider.enabled = false;
        groundPoundLandCollider.enabled = false;
    }

    public void ResetGroundPound() {
        canInitiateDifferentAction = false;
        StopCoroutine(start);
        EndGroundPoundBuildUp();
        starting = false;
        rigidBody.useGravity = true;
        groundPoundCollider.enabled = false;
        groundPoundLandCollider.enabled = false;
        finishedGroundPound = false;
        FinishGroundPound();
    }
}

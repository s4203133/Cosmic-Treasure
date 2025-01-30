using System;
using UnityEngine;

public class PlayerHover : MonoBehaviour {
    [Header("HOVER SETTINGS")]
    [SerializeField] private float maxHoverDuration;
    private float timer;
    public bool finished => timer <= 0;

    [SerializeField] private float airBoost;
    [SerializeField] private float airBoostTime;
    private float airBoostTimer;
    private float yVelocityLimit;

    private bool canHover;
    public bool CheckCanHover => canHover;

    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody rigidBody;

    public Action OnHoverStarted;

    private void OnEnable() {
        EnableHover();
        Grounded.OnLanded += EnableHover;
    }

    private void OnDisable() {
        Grounded.OnLanded -= EnableHover;
    }

    private void OnDestroy() {
        Grounded.OnLanded -= EnableHover;
    }

    public void StartHover() {
        OnHoverStarted?.Invoke();

        canHover = false;
        rigidBody.velocity = Vector3.zero;
        timer = maxHoverDuration;
        airBoostTimer = airBoostTime;
    }

    public void ApplyHoverForce() {
        ApplyAirBoost();
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, yVelocityLimit, rigidBody.velocity.z);
        timer -= Time.fixedDeltaTime;
    }

    public void ApplyAirBoost() {
        yVelocityLimit = rigidBody.velocity.y + airBoost;
        airBoostTimer -= Time.fixedDeltaTime;
        if (airBoostTimer <= 0) {
            yVelocityLimit = 1;
        }
    }

    public void CuttOffHover() {
        timer = 0;
        airBoostTimer = 0;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
    }

    public void EnableHover() {
        canHover = true;
    }
}
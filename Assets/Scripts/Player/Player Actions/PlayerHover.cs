using System;
using UnityEngine;

public class PlayerHover : MonoBehaviour {
    [Header("HOVER SETTINGS")]
    [SerializeField] private float maxHoverDuration;
    private float timer;
    public bool finished => timer <= 0;
    private bool hoverEnded;

    [SerializeField] private AnimationCurve AirForce;
    [SerializeField] private float airBoost;
    private float airBoostTimer;
    private float yVelocityLimit;

    private bool canHover;
    public bool CheckCanHover => canHover;

    [Header("COMPONENTS")]
    [SerializeField] private Rigidbody rigidBody;

    public Action OnHoverStarted;
    public Action OnHoverEnded;

    public void StartHover() {
        OnHoverStarted?.Invoke();

        canHover = false;
        hoverEnded = false;
        rigidBody.velocity = Vector3.zero;
        timer = maxHoverDuration;
        airBoostTimer = 0;
    }

    public void ApplyHoverForce() {
        ApplyAirBoost();
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, yVelocityLimit, rigidBody.velocity.z);
        timer -= Time.fixedDeltaTime;
    }

    public void ApplyAirBoost() {
        float time = (airBoostTimer * (100f / maxHoverDuration)) / 100f;
        yVelocityLimit = AirForce.Evaluate(time) * airBoost;
        airBoostTimer += Time.fixedDeltaTime;
    }

    public void CuttOffHover() {
        timer = 0;
        airBoostTimer = maxHoverDuration;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
    }

    public void EndHover() {
        if (!hoverEnded) {
            OnHoverEnded?.Invoke();
            hoverEnded = true;
        }
    }

    public void EnableHover() {
        canHover = true;
    }
}
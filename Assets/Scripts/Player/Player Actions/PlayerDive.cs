using System;
using UnityEngine;

public class PlayerDive : MonoBehaviour
{
    [Header("DIVE SETTINGS")]
    [SerializeField] private float diveForce;
    [SerializeField] private AnimationCurve diveMomentum;
    private float timer;
    private float velocity;
    [SerializeField] private float initialAirBoost;

    [Header("COMPONENTS")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody rigidBody;

    public Action OnDive;

    public void StartDive() {
        OnDive?.Invoke();
        timer = 0;
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, initialAirBoost, rigidBody.velocity.z);
    }

    public void Countdown() {
        timer += Time.deltaTime;
    }

    public void ApplyDiveForce() {
        GetVelocity();
        Vector3 newVelocity = playerTransform.forward * (velocity * diveForce * Time.deltaTime);
        newVelocity.y = rigidBody.velocity.y;
        rigidBody.velocity = newVelocity;
    }

    private void GetVelocity() {
        velocity = diveMomentum.Evaluate(timer);
    }
}

using System;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class Crate : MonoBehaviour, IBreakable {

        [Header("BREAKABLE SETTINGS")]
        [SerializeField] private VisualEffect breakParticles;
        private float breakParticlesDuration;

        [SerializeField] private GameObject brokenPiecesParent;
        [SerializeField] private Rigidbody[] brokenPieces;

        [SerializeField] private Vector2 breakForceRange;
        [SerializeField] private float breakTorqueRange;

        public static Action HitObject;

        private void Awake() {
            breakParticlesDuration = breakParticles.GetVector2("LifeTimeRange").y;
        }

        public void Break() {
            IBreakable.OnBroken?.Invoke();
            HitObject?.Invoke();

            // Activate the broken pieces and apply force to them
            brokenPiecesParent.transform.parent = null;
            brokenPiecesParent.SetActive(true);
            ApplyForceToBrokenPieces();

            // Spawn VFX
            breakParticles.transform.parent = null;
            breakParticles.Play();

            // Destroy VFX after delay, and crate game object
            Destroy(breakParticles.gameObject, breakParticlesDuration);
            Destroy(gameObject);
        }

        // Apply force to each broken piece in a random direction
        private void ApplyForceToBrokenPieces() {
            for (int i = 0; i < brokenPieces.Length; i++) {
                Vector3 forceDirection = (brokenPieces[i].transform.position - transform.position).normalized;
                brokenPieces[i].AddForce(forceDirection * UnityEngine.Random.Range(breakForceRange.x, breakForceRange.y), ForceMode.Impulse);
                brokenPieces[i].AddTorque(RandomTorqueValue(), ForceMode.Impulse);
            }
        }

        // Apply random torque to each broken piece to make them spin
        private Vector3 RandomTorqueValue() {
            float RandomNumber() {
                return UnityEngine.Random.Range(-breakTorqueRange, breakTorqueRange);
            }
            return new Vector3(RandomNumber(), RandomNumber(), RandomNumber());
        }
    }
}
using UnityEngine;
using UnityEngine.VFX;

public class Crate : MonoBehaviour, IBreakable {

    [Header("BREAKABLE SETTINGS")]
    [SerializeField] private VisualEffect breakParticles;
    private float breakParticlesDuration;

    [SerializeField] private GameObject brokenPiecesParent;
    [SerializeField] private Rigidbody[] brokenPieces;

    [SerializeField] private Vector2 breakForceRange;
    [SerializeField] private float breakTorqueRange;

    private void Awake() {
        breakParticlesDuration = breakParticles.GetVector2("LifeTimeRange").y;
    }

    public void Break() {
        IBreakable.OnBroken?.Invoke();

        brokenPiecesParent.transform.parent = null;
        brokenPiecesParent.SetActive(true);

        breakParticles.transform.parent = null;
        breakParticles.Play();
  
        ApplyForceToBrokenPieces();

        Destroy(breakParticles.gameObject, breakParticlesDuration);
        Destroy(gameObject);
    }


    private Vector3 RandomTorqueValue() {
        float RandomNumber() {
            return Random.Range(-breakTorqueRange, breakTorqueRange);
        }
        return new Vector3(RandomNumber(), RandomNumber(), RandomNumber());
    }

    private void ApplyForceToBrokenPieces() {
        for(int i = 0; i < brokenPieces.Length; i++) {
            Vector3 forceDirection = (brokenPieces[i].transform.position - transform.position).normalized;
            brokenPieces[i].AddForce(forceDirection * Random.Range(breakForceRange.x, breakForceRange.y), ForceMode.Impulse);
            brokenPieces[i].AddTorque(RandomTorqueValue(), ForceMode.Impulse);
        }
    }
}

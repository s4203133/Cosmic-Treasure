using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Coin : MonoBehaviour
{
    private Transform thisTransform;
    private Vector3 position;

    [SerializeField] private float spawnAnimateDuration;

    [Header("COLLECT VFX")]
    [SerializeField] private VisualEffect collectedVFX;
    private float collectedVFXDuration;

    private Collider coinCollider;

    private void Awake() {
        thisTransform = transform;
        coinCollider = GetComponent<Collider>();
        collectedVFXDuration = collectedVFX.GetVector2("LifetimeRange").y;
    }

    private void FixedUpdate() {
        AnimateCoin();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            collectedVFX.transform.parent = null;
            collectedVFX.Play();
            Destroy(collectedVFX.gameObject, collectedVFXDuration);
            Destroy(gameObject);
        }
    }

    public void SetPosition(Vector3 newPosition) {
        position = newPosition;
        StartCoroutine(ActivateCollision());
    }

    private void AnimateCoin() {
        thisTransform.position = Vector3.Lerp(thisTransform.position, position, spawnAnimateDuration);
    }

    private IEnumerator ActivateCollision() {
        yield return new WaitForSeconds(0.75f);
        coinCollider.enabled = true;
    }
}

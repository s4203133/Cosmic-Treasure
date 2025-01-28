using System.Collections;
using UnityEngine;

public class HighJumpTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float duration;
    [SerializeField] private PlayerJump jump;

    private void OnEnable() {
        jump.OnHighJump += StartTrail;
    }

    private void OnDisable() {
        jump.OnHighJump -= StartTrail;
    }

    private void StartTrail() {
        trailRenderer.emitting = true;
        StartCoroutine(StopTrail());
    }

    private IEnumerator StopTrail() {
        yield return new WaitForSeconds(duration);
        trailRenderer.emitting = false;
    }
}

using System.Collections;
using UnityEngine;

namespace LMO.Player {

    public class HighJumpTrail : MonoBehaviour {
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private float duration;
        [SerializeField] private PlayerJump jump;

        public void StartTrail() {
            trailRenderer.emitting = true;
            StartCoroutine(StopTrail());
        }

        private IEnumerator StopTrail() {
            yield return new WaitForSeconds(duration);
            trailRenderer.emitting = false;
        }
    }
}
using System.Collections;
using UnityEngine;
using LMO;

namespace NR {
    /// <summary>
    /// Explosion that appears when an enemy projectile hits the ground.
    /// Scales over time, and interpolated between supplied colours.
    /// While enabled, this will kill the player on contact.
    /// </summary>
    public class ProjectileExplosion : MonoBehaviour {
        [SerializeField]
        private Color[] colours;

        [SerializeField]
        private float duration;

        [SerializeField]
        private float hurtEndTime;

        [SerializeField]
        private Vector3 endScale;

        private Vector3 startScale;

        private bool hurtEnabled;
        private Renderer _renderer;

        private void Awake() {
            startScale = transform.localScale;
            _renderer = GetComponent<Renderer>();
        }

        public void ShowExplosion(Vector3 pos) {
            gameObject.SetActive(true);
            transform.position = pos;
            hurtEnabled = true;
            StartCoroutine(AnimateExplosion());
        }

        private IEnumerator AnimateExplosion() {
            float currentTime = 0;
            float colourDivision = duration / (colours.Length - 1);
            float scaleDivision = 1 / duration;
            Color startColour = colours[0];
            Color endColour = colours[1];
            int colourIndex = 0;

            while (currentTime < duration) {
                // Divides the interpolation into shorter lerps between each colour in the sequence.
                float colourTime = (currentTime % colourDivision) / colourDivision;
                Color newColour = Color.Lerp(startColour, endColour, colourTime);

                _renderer.material.color = newColour;

                float perc = 1 - currentTime * scaleDivision;
                perc = 1 - (perc * perc * perc);
                transform.localScale = Vector3.Lerp(startScale, endScale, perc);
                yield return null;
                currentTime += Time.deltaTime;
                if (currentTime > hurtEndTime) {
                    hurtEnabled = false;
                }
                int newIndex = Mathf.FloorToInt(currentTime / colourDivision);
                if (newIndex > colourIndex && newIndex < colours.Length - 1) {
                    colourIndex = newIndex;
                    startColour = colours[colourIndex];
                    endColour = colours[colourIndex + 1];
                }
            }
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if (!hurtEnabled) {
                return;
            }

            if (other.tag == "Player") {
                LevelDeathCatcher.OnPlayerFellOutLevel?.Invoke();
                //May replace with damage if instant-kill is too unfair.
            }
        }
    }
}
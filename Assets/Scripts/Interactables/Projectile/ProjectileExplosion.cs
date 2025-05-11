using System.Collections;
using UnityEngine;
using LMO;

namespace NR {
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
        private float currentTime;
        private Renderer _renderer;

        private void Awake() {
            startScale = transform.localScale;
            _renderer = GetComponent<Renderer>();
        }

        public void ShowExplosion(Vector3 pos) {
            gameObject.SetActive(true);
            transform.position = pos;
            currentTime = 0;
            hurtEnabled = true;
            StartCoroutine(AnimateExplosion());
        }

        private IEnumerator AnimateExplosion() {
            float colourDivision = duration / colours.Length;
            float scaleDivision = 1 / duration;
            while (currentTime < duration) {
                int colourIndex = Mathf.FloorToInt(currentTime / colourDivision);
                float colourTime = (currentTime % colourDivision) * duration;
                
                Color newColour = colours[colours.Length - 1];
                if (colourIndex < colours.Length - 1) {
                    Color startColour = colours[colourIndex];
                    Color endColour = colours[colourIndex + 1];
                    newColour = Color.Lerp(startColour, endColour, colourTime);
                }

                _renderer.material.color = newColour;

                float perc = 1 - currentTime * scaleDivision;
                perc = 1 - (perc * perc * perc);
                transform.localScale = Vector3.Lerp(startScale, endScale, perc);
                yield return null;
                currentTime += Time.deltaTime;
                if (currentTime > hurtEndTime) {
                    hurtEnabled = false;
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
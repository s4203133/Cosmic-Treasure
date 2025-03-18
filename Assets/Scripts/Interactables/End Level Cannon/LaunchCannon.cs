using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class LaunchCannon : MonoBehaviour {
        [SerializeField] private GameObject cannonPlayer;

        [SerializeField] private VisualEffect launchVFX;

        [SerializeField] private float endSceneAfterTime;
        private WaitForSeconds endScene;

        [SerializeField] private EndScreen endScreen;
        [SerializeField] private SceneIndexData sceneIndexData;

        private bool launched;

        private void Start() {
            endScene = new WaitForSeconds(endSceneAfterTime);
        }

        public void Launch() {
            if (launched) {
                return;
            }

            cannonPlayer.SetActive(true);
            if (launchVFX != null) {
                launchVFX.Play();
            }
            launched = true;
            StartCoroutine(EndScene());
        }

        private IEnumerator EndScene() {
            yield return endScene;
            endScreen.Open();
            SceneLoadManager.instance.LoadScene(sceneIndexData.EndLevelScene);
        }
    }
}
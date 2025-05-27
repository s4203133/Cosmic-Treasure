using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace LMO {

    public class LaunchCannon : MonoBehaviour {
        [SerializeField] private int sceneToLoad;
        [SerializeField] private GameObject cannonPlayer;

        [SerializeField] private VisualEffect launchVFX;

        [SerializeField] private float endSceneAfterTime;
        private WaitForSeconds endScene;

        [SerializeField] private EndScreen endScreen;
        [SerializeField] private FadeScreen fadeScreen;
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
            fadeScreen.gameObject.SetActive(false);
            endScreen.Open();
            if (sceneToLoad < 0) {
                SceneLoadManager.instance.LoadScene(sceneIndexData.EndLevelScene);
            }
            else {
                SceneLoadManager.instance.LoadScene(sceneToLoad);
            }
        }
    }
}
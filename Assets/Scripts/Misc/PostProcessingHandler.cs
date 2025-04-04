using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LMO {

    public class PostProcessingHandler : MonoBehaviour {
        [SerializeField] private Volume mainPostProcessVolume;

        [SerializeField] private Volume blurredPostProcessVolume;
        private DepthOfField blur;

        void Start() {
            if (blurredPostProcessVolume.profile.TryGet(out DepthOfField tmp)) {
                blur = tmp;
            }
        }

        public void BlurScreen() {
            StartCoroutine(BlurScreenLogic());
        }

        public void ClearBlurScreen() {
            StartCoroutine(ClearBlurScreenLogic());
        }

        private IEnumerator BlurScreenLogic() {
            float t = 0;
            while (t < 1.0f) {
                t += TimeValues.Delta;
                SetBlur(t);
                yield return null;
            }
        }

        private IEnumerator ClearBlurScreenLogic() {
            float t = 1;
            while (t > 0.0f) {
                t -= TimeValues.Delta;
                SetBlur(t);
                yield return null;
            }
        }

        private void SetBlur(float value) {
            blurredPostProcessVolume.weight = value;
            blur.gaussianMaxRadius.value = value + 0.5f;
        }
    }
}
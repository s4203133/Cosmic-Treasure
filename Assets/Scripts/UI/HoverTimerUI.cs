using UnityEngine;
using UnityEngine.UI;

namespace LMO {

    public class HoverTimerUI : MonoBehaviour {
        [SerializeField] private FloatVariable hoverTimer;
        [SerializeField] private FloatVariable hoverDuration;
        private Slider hoverTimerSlider;
        [Range(0f, 1f)]
        [SerializeField] private float sliderCountdownSmoothness;

        private Animator sliderAnim;

        private bool active;

        private void OnEnable() => SubscribeEvents();

        private void OnDisable() => UnsubscribeEvents();

        private void Start() {
            hoverTimerSlider = GetComponent<Slider>();
            sliderAnim = GetComponent<Animator>();
        }

        private void Update() {
            float time = (hoverTimer.value * (100f / hoverDuration.value)) / 100f;
            hoverTimerSlider.value = Mathf.Lerp(hoverTimerSlider.value, time, sliderCountdownSmoothness);
        }

        private void SubscribeEvents() {
            PlayerHover.OnHoverStarted += EnableTimer;
            Grounded.OnLanded += DisableTimer;
        }

        private void UnsubscribeEvents() {
            PlayerHover.OnHoverStarted -= EnableTimer;
            Grounded.OnLanded -= DisableTimer;
        }

        public void EnableTimer() {
            sliderAnim.SetTrigger("Open");
            hoverTimerSlider.value = hoverDuration.value;
            active = true;
        }

        public void DisableTimer() {
            if (active) {
                sliderAnim.SetTrigger("Close");
                active = false;
            }
        }
    }
}
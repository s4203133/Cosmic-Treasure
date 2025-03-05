using UnityEngine;

namespace LMO {

    public class ControllerRumbleManager : MonoBehaviour {
        [SerializeField] private ControllerRumbles controllerRumbles;

        private void OnEnable() {
            PlayerSpinState.OnSpin += ShortRumble;
        }

        private void OnDisable() {
            PlayerSpinState.OnSpin -= ShortRumble;
        }

        private void RumbleController(GamepadRumble rumble) {
            rumble.Rumble();
            StartCoroutine(rumble.StopRumble());
        }

        void ShortRumble() {
            RumbleController(controllerRumbles.Short);
        }
    }
}

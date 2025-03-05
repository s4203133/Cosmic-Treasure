using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class ControllerRumbles {
        [SerializeField] private GamepadRumble shortRumble;
        [SerializeField] private GamepadRumble mediumRumble;
        [SerializeField] private GamepadRumble longRumble;

        public GamepadRumble Short => shortRumble;
        public GamepadRumble Medium => mediumRumble;
        public GamepadRumble Long => longRumble;
    }
}
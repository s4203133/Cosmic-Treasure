using UnityEngine;

namespace LMO {

    [System.Serializable]
    public class ControllerRumbles {
        [SerializeField] private GamepadRumble tinyRumble;
        [SerializeField] private GamepadRumble shortRumble;
        [SerializeField] private GamepadRumble mediumRumble;
        [SerializeField] private GamepadRumble longRumble;

        public GamepadRumble Tiny => tinyRumble;
        public GamepadRumble Short => shortRumble;
        public GamepadRumble Medium => mediumRumble;
        public GamepadRumble Long => longRumble;
    }
}
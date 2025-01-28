using UnityEngine;

public class ControllerRumbleManager : MonoBehaviour
{
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

[System.Serializable]
public class ControllerRumbles {
    [SerializeField] private GamepadRumble shortRumble;
    [SerializeField] private GamepadRumble mediumRumble;
    [SerializeField] private GamepadRumble longRumble;

    public GamepadRumble Short => shortRumble;
    public GamepadRumble Medium => mediumRumble;
    public GamepadRumble Long => longRumble;
}

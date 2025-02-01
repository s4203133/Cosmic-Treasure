using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Controller/Controller Rumble", fileName = "New Controller Rumble")]
public class GamepadRumble : ScriptableObject {
    [SerializeField] private float lowFrequency;
    [SerializeField] private float highFrequency;
    [SerializeField] private float duration;

    private Gamepad gamePad;

    public void Rumble() {

        // May need to do checks if the controls scheme changes and a gamepad is no longer connected

        gamePad = Gamepad.current;
        if (gamePad != null) {
            gamePad.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }

    public IEnumerator StopRumble() {
        if(gamePad == null) {
            yield break;
        }
        // Rumble the controller for the specified duration
        float t = 0;
        while (t < duration) {
            t += Time.deltaTime;
            yield return null;
        }
        gamePad.SetMotorSpeeds(0, 0);
    }
}


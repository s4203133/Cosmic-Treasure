using UnityEngine;

[System.Serializable]
public class InputBuffer {
    private bool inputPressed;
    public float length;
    private float countdown = 0;

    public InputBuffer(float length) {
        this.length = length;
    }

    public void Countdown() {
        // If the input has been recently, reduce a timer until it is disabled
        // (allows the input to still be recognised for a short period of time)
        if (inputPressed) {
            countdown -= Time.deltaTime;
            if (countdown <= 0) {
                inputPressed = false;
            }
        }
    }

    public void PressInput() {
        inputPressed = true;
        countdown = length;
    }

    public bool HasInputBeenRecieved() {
        return inputPressed;
    }
}
using UnityEngine;

public class InputBuffers : MonoBehaviour {
    public InputBuffer jumpInputBuffer;

    private void OnEnable() {
        SubscribeInput();
    }

    private void OnDisable() {
        UnsubscribeInput();
    }

    private void Update() {
        jumpInputBuffer.Countdown();
    }

    private void SubscribeInput() {
        InputHandler.jumpPerformed += jumpInputBuffer.PressInput;
    }

    private void UnsubscribeInput() {
        InputHandler.jumpPerformed -= jumpInputBuffer.PressInput;
    }
}

[System.Serializable]
public class InputBuffer {
    private bool inputPressed;
    public float length;
    private float countdown = 0;

    public InputBuffer(float length) {
        this.length = length;
    }

    public void Countdown() {
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
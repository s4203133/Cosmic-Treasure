using UnityEngine;

public class InputBuffers : MonoBehaviour {

    [SerializeField] private InputBuffer jumpInputBuffer;
    [SerializeField] private InputBuffer spinInputBuffer;

    public InputBuffer jump => jumpInputBuffer;
    public InputBuffer spin => spinInputBuffer;

    private void OnEnable() {
        SubscribeInput();
    }

    private void OnDisable() {
        UnsubscribeInput();
    }

    private void Update() {
        jumpInputBuffer.Countdown();
        spinInputBuffer.Countdown();
    }

    private void SubscribeInput() {
        InputHandler.jumpPerformed += jumpInputBuffer.PressInput;
        InputHandler.SpinStarted += spinInputBuffer.PressInput;
    }

    private void UnsubscribeInput() {
        InputHandler.jumpPerformed -= jumpInputBuffer.PressInput;
        InputHandler.SpinStarted -= spinInputBuffer.PressInput;
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
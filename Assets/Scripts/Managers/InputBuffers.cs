using UnityEngine;
using LMO;

public class InputBuffers : MonoBehaviour {

    public static InputBuffers instance;

    [SerializeField] private InputBuffer jumpInputBuffer;
    [SerializeField] private InputBuffer spinInputBuffer;

    public InputBuffer jump => jumpInputBuffer;
    public InputBuffer spin => spinInputBuffer;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("There are multiple Input Buffers in the scene. Please Ensure there are only one.", this);
            Destroy(this);
        } else {
            instance = this;
        }
    }

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

using UnityEngine;

public class PauseManager : MonoBehaviour
{

    private void OnEnable() {
        SubscribeInput();
    }

    private void OnDisable() {
        UnsubscribeInput();
    }

    private void SubscribeInput() {
        InputHandler.selectedStarted += GlobalEventManager.OnSceneRestarted;
        // InputHandler.selectedStarted += SceneLoadManager.instance.RestartScene;
    }

    private void UnsubscribeInput() {
        InputHandler.selectedStarted -= GlobalEventManager.OnSceneRestarted;
        // InputHandler.selectedStarted -= SceneLoadManager.instance.RestartScene;

    }
}

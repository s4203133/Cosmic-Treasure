using LMO;
using System.Collections;
using UnityEngine;

public class EndSceneInput : MonoBehaviour
{
    [SerializeField] private float activateInputAfterTime;
    private WaitForSeconds delay;

    [Space(15)]
    [SerializeField] private SceneLoadManager sceneLoadManager;
    [SerializeField] private SceneIndexData sceneIndexData;

    private void OnEnable() {
        delay = new WaitForSeconds(activateInputAfterTime);
        StartCoroutine(ActivateInput());
    }

    private void OnDisable() {
        UnsubscribeInput();
    }

    private IEnumerator ActivateInput() {
        yield return delay;
        SubscribeInput();
    }

    private void SubscribeInput() {
        InputHandler.jumpStarted += LoadWorldMap;
    }

    private void UnsubscribeInput() {
        InputHandler.jumpStarted -= LoadWorldMap;
    }

    private void LoadWorldMap() {
        sceneLoadManager.LoadScene(sceneIndexData.WorldMapScene);
    }
}

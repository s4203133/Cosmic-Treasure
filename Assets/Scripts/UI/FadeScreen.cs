using LMO;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private bool hiddenOnStart;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        if (hiddenOnStart) {
            animator.SetTrigger("Hidden");
        }
    }

    private void OnEnable() {
        SubscribeToEvents();
    }

    private void OnDisable() {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents() {
        SceneLoadManager.OnSceneStartedLoading += FadeIn;
        GlobalEventManager.SceneRestarted += FadeIn;
        SpawnPlayer.OnLevelReset += FadeIn;
        SpawnPlayer.OnPlayerRespawned += FadeOut;
    }

    private void UnsubscribeFromEvents() {
        SceneLoadManager.OnSceneStartedLoading -= FadeIn;
        GlobalEventManager.SceneRestarted -= FadeIn;
        SpawnPlayer.OnLevelReset -= FadeIn;
        SpawnPlayer.OnPlayerRespawned -= FadeOut;
    }

    public void FadeIn() {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut() {
        animator.SetTrigger("FadeOut");
    }
}

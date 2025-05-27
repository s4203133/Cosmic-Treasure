using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    private FadeScreen fade;
    [SerializeField] private float timeToCloseScene;
    private float timer;
    private bool startedClose;

    void Start()
    {
        fade = FindObjectOfType<FadeScreen>();
        timer = timeToCloseScene;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 1.1) {
            FadeSceneOut();
            if(timer < 0) {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void FadeSceneOut() {
        if (!startedClose) {
            fade.FadeIn();
            startedClose = true;
        }
    }
}

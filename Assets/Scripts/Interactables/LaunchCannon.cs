using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class LaunchCannon : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Vector3 launchVelocity;

    [SerializeField] private VisualEffect launchVFX;

    private WaitForSeconds endScene;
    [SerializeField] private Animator endScreen;

    private void Start()
    {
        endScene = new WaitForSeconds(0.25f);
    }

    private bool launched;

    public void Launch()
    {
        if (launched)
        {
            return;
        }

        playerRigidBody.gameObject.SetActive(true);
        playerRigidBody.velocity = launchVelocity;
        if(launchVFX != null ) {
            launchVFX.Play();
        }
        launched = true;
        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        yield return endScene;
        endScreen.gameObject.SetActive(true);
        endScreen.SetTrigger("FadeIn");
    }
}

using System.Collections;
using UnityEngine;

public class TutorialGuide : MonoBehaviour
{
    private Transform thisTransform;
    private Animator animator;

    private void Start() {
        thisTransform = transform;
        animator = GetComponent<Animator>();
    }

    public void ___SetTranslation(Transform referenceTransform) {
        thisTransform.position = referenceTransform.position;
        thisTransform.rotation = referenceTransform.rotation;
        thisTransform.localScale = referenceTransform.localScale;
    }
    public void ___SetAnimationTrigger(string name) {
        animator.SetTrigger(name);
        TutorialGuideSequence.progressSequence?.Invoke();
    }

    public void ___SetTutorialText(string newText) {
        // DO LOGIC...
        TutorialGuideSequence.progressSequence?.Invoke();
    }

    public void ___Wait(float duration) {
        StartCoroutine(WaitLogic(duration));
    }

    private IEnumerator WaitLogic(float duration) {
        WaitForSeconds delay = new WaitForSeconds(duration);
        yield return delay;
        TutorialGuideSequence.progressSequence?.Invoke();
    }

    public void ___DebugMessage(string message) {
        Debug.Log(message);
        TutorialGuideSequence.progressSequence?.Invoke();
    }
}

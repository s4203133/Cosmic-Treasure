using LMO;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI instructionText;
    private Animator animator;

    [SerializeField] private float writeSpeed;
    [SerializeField] private float pauseSpeed;
    private WaitForSeconds writeDelay;
    private WaitForSeconds pauseDelay;

    private string targetMessage;

    public string testMessage;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        instructionText = GetComponentInChildren<TextMeshProUGUI>();
        writeDelay = new WaitForSeconds(writeSpeed);
        pauseDelay = new WaitForSeconds(pauseSpeed);
        WriteInstruction(testMessage);
    }

    public void Open() {
        animator.SetTrigger("FadeIn");
    }

    public void Close() {
        animator.SetTrigger("FadeOut");
    }

    public void WriteInstruction(string message) {
        targetMessage = message;
        StartCoroutine(WriteText());
    }

    private IEnumerator WriteText() {
        yield return new WaitForSeconds(0.1f);
        instructionText.text = "";
        string newText = "";
        for(int i = 0;  i < targetMessage.Length; i++) {
            newText += targetMessage[i].ToString();
            if (targetMessage[i].ToString() == " ") {
                continue;
            }
            yield return writeDelay;
            instructionText.text = newText;

            if (targetMessage[i].ToString() == ".") {
                yield return pauseDelay;
                continue;
            }
        }
    }
}

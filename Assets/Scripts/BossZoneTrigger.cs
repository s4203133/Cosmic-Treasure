using UnityEngine;
using TMPro;

public class BossZoneTrigger : MonoBehaviour
{
    public TextMeshProUGUI bossTitleText; // Drag your Text UI here
    public float displayTime = 3f;        // How long to show the message

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the text
            bossTitleText.gameObject.SetActive(true);
            bossTitleText.text = "Pirate Boss Battle";

            // Hide it after a few seconds
            Invoke("HideTitle", displayTime);
        }
    }

    void HideTitle()
    {
        bossTitleText.gameObject.SetActive(false);
    }
}

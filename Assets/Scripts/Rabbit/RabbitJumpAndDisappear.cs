using UnityEngine;

public class RabbitJumpAndDisappear : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpDuration = 0.5f;

    private bool isCollected = false;
    private float jumpTime = 0f;
    private Vector3 startPos;
    private Vector3 targetPos;

    private AudioSource audioSource; // 🔊 Sound component

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + new Vector3(0, jumpHeight, 0);

        // 🔊 Get the AudioSource from the rabbit
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerJumpAndDisappear()
    {
        if (!isCollected)
        {
            isCollected = true;
            startPos = transform.position;
            targetPos = startPos + Vector3.up * jumpHeight;

            // 🔊 Play the sound if one is assigned
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    void Update()
    {
        if (isCollected)
        {
            jumpTime += Time.deltaTime;

            // Smooth jump upward
            float progress = Mathf.Clamp01(jumpTime / jumpDuration);
            transform.position = Vector3.Lerp(startPos, targetPos, progress);

            if (progress >= 1f)
            {
                // After reaching top, destroy after short delay
                Destroy(gameObject, 0.3f);
            }
        }
    }
}

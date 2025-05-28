using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Play() {
        audioSource.pitch = Random.Range(0.88f, 1.2f);
        audioSource.Play();
    }
}

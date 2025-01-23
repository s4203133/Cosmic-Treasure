using UnityEngine;

public class Crate : MonoBehaviour, IBreakable {

    [Header("BREAKABLE SETTINGS")]
    [SerializeField] private GameObject breakParticles;

    public void Break() {
        Destroy(gameObject);
    }
}

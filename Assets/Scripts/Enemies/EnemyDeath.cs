using UnityEngine;
using LMO;

public class EnemyDeath : MonoBehaviour, ISpinnable {
    [SerializeField] private GameObject Enemy;
    public float SlimeHealth;
    private float CurrentSlimeHealth;
    private float timer = 0;
    private bool canhit;

    private void Start() {
        CurrentSlimeHealth = SlimeHealth;
        timer = 0;
        canhit = true;
    }
    public void OnHit() {
        if (canhit == true) {
            SlimeHealth -= 10;
            canhit = false;
        }

        
    }
    public void Update() {
        if (SlimeHealth <= 0) {
            Destroy(Enemy);
        }
        if (canhit == false) {
            timer += Time.deltaTime;
            if (timer >= 2) {
                canhit = true;
                timer = 0;
            }
        }
    }
}

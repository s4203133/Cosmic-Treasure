using UnityEngine;
using LMO;

public class EnemyDeath : MonoBehaviour, ISpinnable {
    [SerializeField] private GameObject Enemy;
    public float SlimeHealth;
    private float CurrentSlimeHealth;
    private float timer = 0;
    private bool canhit;
    public Animator SlimeAnims;
    private float DeathTimer;

    private void Start() {
        CurrentSlimeHealth = SlimeHealth;
        timer = 0;
        canhit = true;
        DeathTimer = 0;
    }
    public void OnHit() {
        if (canhit == true) {
            SlimeHealth -= 10;
            canhit = false;
        }

        
    }
    public void Update() {
        if (SlimeHealth <= 0) {
            SlimeAnims.SetBool("SlimeDead", true);
            DeathTimer += Time.deltaTime;
            if (DeathTimer >= 1.2f) {
                Destroy(Enemy);
            }
            
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

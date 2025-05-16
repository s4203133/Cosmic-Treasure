using UnityEngine;
using LMO;

public class EnemyDeath : MonoBehaviour, ISpinnable, IResettable {
    [SerializeField] private GameObject Enemy;
    public float SlimeHealth;
    private float CurrentSlimeHealth;
    private float timer = 0;
    private bool canhit;
    public Animator SlimeAnims;
    private float DeathTimer;
    private float StartingHealth;

    private void Start() {
        CurrentSlimeHealth = SlimeHealth;
        timer = 0;
        canhit = true;
        DeathTimer = 0;
        StartingHealth = SlimeHealth;
    }
    public void OnHit() {
        if (canhit == true) {
            SlimeHealth -= 10;
            canhit = false;
        }

        
    }
    public void Reset() {
        SlimeHealth = StartingHealth;
        Debug.Log(SlimeHealth);
    }
    public void Update() {
        if (SlimeHealth <= 0) {
            SlimeAnims.SetBool("SlimeDead", true);
            DeathTimer += Time.deltaTime;
            if (DeathTimer >= 1.2f) {
                //Destroy(Enemy);
                Enemy.SetActive(false);
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

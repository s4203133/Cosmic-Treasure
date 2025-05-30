using UnityEngine;
using LMO;
using UnityEngine.AI;
using System;

public class EnemyDeath : MonoBehaviour, ISpinnable {
    [SerializeField] private GameObject Enemy;
    private NavMeshAgent agent;
    private SphereCollider enemyCollider;
    
    public float SlimeHealth;
    private float CurrentSlimeHealth;
    private float timer = 0;
    private bool canhit;
    public Animator SlimeAnims;
    private float DeathTimer;
    private float StartingHealth;

    public static Action OnEnemyHit;

    private void Start() {
        agent = GetComponentInParent<NavMeshAgent>();
        enemyCollider = GetComponent<SphereCollider>();

        CurrentSlimeHealth = SlimeHealth;
        timer = 0;
        canhit = true;
        DeathTimer = 0;
        StartingHealth = SlimeHealth;
    }
    public void OnHit() {
        agent.isStopped = true;
        enemyCollider.enabled = false;
        SlimeAnims.SetBool("SlimeDead", true);
        OnEnemyHit?.Invoke();

        //if (canhit == true) {
        //    SlimeHealth -= 10;
        //    canhit = false;
        //}


    }
    //public void Reset() {
    //    if(agent == null) {
    //        Debug.Log("No Agent On This Enemy!", gameObject);
    //        return;
    //    }
    //    agent.isStopped = false;
    //    enemyCollider.enabled = true;
    //    SlimeHealth = StartingHealth;
    //    SlimeAnims.SetBool("SlimeDead", false);
    //    //DeathTimer = 0;
    //    canhit = true;
    //}

    public void Update() {
        if (SlimeHealth <= 0) {
            agent.isStopped = true;
            enemyCollider.enabled = false;
            SlimeAnims.SetBool("SlimeDead", true);

            //DeathTimer += Time.deltaTime;
            //if (DeathTimer >= 1.2f) {
            //    Enemy.SetActive(false);
            //}
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

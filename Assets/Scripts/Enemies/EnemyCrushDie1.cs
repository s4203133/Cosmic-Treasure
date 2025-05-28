using System;
using UnityEngine;
using UnityEngine.AI;
using LMO;

namespace NR {
    public class EnemyCrushDie1 : MonoBehaviour, ICrushable {
        private NavMeshAgent agent;
        private GameObject enemy;
        private SphereCollider enemyCollider;

        private bool hasBeenCrushed;
        private float deathTimer;

        public Animator SlimeAnims;

        public static Action OnEnemyHit;

        private void Start() {
            agent = GetComponentInParent<NavMeshAgent>();
            enemyCollider = GetComponent<SphereCollider>();
            enemy = agent.gameObject;
            deathTimer = 0;
        }

        public void OnHit() {
            agent.isStopped = true;
            enemyCollider.enabled = false;
            SlimeAnims.SetBool("SlimeDead", true);
            OnEnemyHit?.Invoke();   
        }

        //public void Reset() {
        //    SlimeAnims.SetBool("SlimeDead", false);
        //    enemyCollider.enabled = true;
        //    agent.isStopped = false;
        //}
    }
}
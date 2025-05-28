using System;
using UnityEngine;
using LMO;

namespace NR {
    public class EnemyCrushDie : MonoBehaviour, ICrushable {
        [SerializeField]private GameObject enemy;        
        public static Action OnEnemyHit;

        public void OnHit() {
            OnEnemyHit?.Invoke();
            enemy.SetActive(false);
        }
    }
}


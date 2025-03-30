using UnityEngine;
using LMO;

namespace NR {
    public class EnemySpinDie : MonoBehaviour, ISpinnable {
        [SerializeField] Enemy1 enemy;
        public void OnHit() {
            //enemy.Die();
        }

    }
}


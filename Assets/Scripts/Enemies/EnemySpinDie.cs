using UnityEngine;
using LMO;
using WWH;

namespace NR {
    public class EnemySpinDie : MonoBehaviour, ISpinnable {
        [SerializeField] private Enemy1 enemy;
        public void OnHit() {
            //enemy.Die();
        }

    }
}


using UnityEngine;
using LMO;

namespace NR {
    public class EnemyCrushDie : MonoBehaviour, ICrushable {
        [SerializeField] Enemy1 enemy;
        public void OnHit() {
            //enemy.Die();
        }

    }
}


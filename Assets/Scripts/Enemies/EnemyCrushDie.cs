using UnityEngine;
using LMO;
using WWH;

namespace NR {
    public class EnemyCrushDie : MonoBehaviour, ICrushable {
        [SerializeField]private GameObject enemy;        
       
        public void OnHit() {
            enemy.SetActive(false);
           
        }

    }
}


using UnityEngine;
using LMO;
using WWH;

namespace NR {
    public class EnemyCrushDie1 : MonoBehaviour, ICrushable {
        [SerializeField]private GameObject Enemy;

        public Animator SlimeAnims;

        public void OnHit()
            {
                SlimeAnims.SetBool("SlimeDead", true);
                Destroy (Enemy, 2);
            }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR {
    public class Projectile : MonoBehaviour {
        public void OnTriggerEnter(Collider other) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Water")) {
                gameObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using UnityEngine;


namespace NR {
    public class MoveablePart : Activateable {
        public Vector3 start;
        public Vector3 end;
         
        public float speed = 1;
        private bool direction;
        private float time;
        private bool moving;

        public override void Activate() {
            direction = !direction;
            if (!moving) {
                StartCoroutine(Move());
            }
        }

        private IEnumerator Move() {
            moving = true;
            Vector3 pos;
            float frameProgress;
            while ((direction && time < 1) || (!direction && time > 0)) {
                pos = Vector3.Lerp(start, end, time);
                transform.position = pos;
                yield return null;
                frameProgress = Time.deltaTime * speed;
                time += direction ? frameProgress : -frameProgress;
            }
            moving = false;
        }
    }

}

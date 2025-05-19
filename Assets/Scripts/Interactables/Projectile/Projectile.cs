using System.Collections;
using UnityEngine;

namespace NR {
    /// <summary>
    /// Behaviour for each projectile, with functionality for launching and hiding when needed.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TrailRenderer))]
    public class Projectile : MonoBehaviour {
        private Rigidbody _rigidbody;
        private TrailRenderer _trailRenderer;
        private bool waitingForEnd;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        public void Initialise(Vector3 pos, Quaternion rot, Vector3 vel) {
            gameObject.SetActive(true);
            
            //transform.localScale = Vector3.one;
            transform.position = pos;
            transform.rotation = rot;

            //_rigidbody.isKinematic = false;
            _rigidbody.velocity = vel;

            _trailRenderer.Clear();
        }

        public void HideSelf(bool immediateHide = true) {
            if (immediateHide) {
                //transform.localScale = Vector3.zero;
                //_rigidbody.isKinematic = true;
                gameObject.SetActive(false);
                waitingForEnd = false;
                return;
            }
            if (waitingForEnd) {
                return;
            }
            float delay = _trailRenderer.time;
            StartCoroutine(HideAfterDelay(delay));
        }

        private IEnumerator HideAfterDelay(float delay) {
            waitingForEnd = true;
            yield return new WaitForSeconds(delay);
            waitingForEnd = false;
            gameObject.SetActive(false);
        }

        protected virtual void OnCollisionEnter(Collision collision) {
            IShootable shootable = collision.gameObject.GetComponent<IShootable>();
            if (shootable != null) {
                shootable.OnShot(this);
            }
        }
    }

}

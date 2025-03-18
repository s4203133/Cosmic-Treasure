using System.Collections;
using UnityEngine;

namespace LMO {

    public class JumpIntoCannon : MonoBehaviour {

        [SerializeField] private Transform playerTransform;

        [Space(15)]
        [SerializeField] private Transform cannonTransform;
        [SerializeField] private Animator cannonAnimator;

        [Space(15)]
        [SerializeField] private AnimationCurve velocity;
        [SerializeField] private AnimationCurve jump;
        private Vector3 originalPosition;
        private float timer;
        private float animationLength;

        public void Play() {
            StartCoroutine(HandleAnimation());
        }

        private IEnumerator HandleAnimation() {
            StartAnimation();

            while (timer < animationLength) {
                timer += Time.deltaTime;
                Vector3 newPosition = Vector3.Lerp(originalPosition, cannonTransform.localPosition, velocity.Evaluate(timer));
                transform.localPosition = new Vector3(newPosition.x, originalPosition.y + jump.Evaluate(timer), newPosition.z);
                yield return null;
            }

            EndAnimation();
            Cannon.OnEnteredCannon?.Invoke();
        }

        private void StartAnimation() {
            timer = 0;
            transform.position = playerTransform.position;
            playerTransform.parent = transform;
            originalPosition = transform.localPosition;
            animationLength = velocity.keys[velocity.length - 1].time;
        }

        private void EndAnimation() {
            cannonAnimator.SetTrigger("Interact");
            playerTransform.parent = null;
            playerTransform.gameObject.SetActive(false);
        }
    }
}
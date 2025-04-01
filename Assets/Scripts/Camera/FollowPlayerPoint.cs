using UnityEngine;

namespace LMO {

    public class FollowPlayerPoint : MonoBehaviour {
        private Transform playerTransform;
        private Transform thisTransform;
        private Grounded playerGrounded;

        [SerializeField] private float followSmoothnessHorizontal;
        [SerializeField] private float followSmoothnessVertical;
        [SerializeField] private float followSmoothnessVerticalGrounded;
        [Tooltip("The max amount of distance between this transform and the players on the Y axis before smoothly adjusting to the player transforms Y position")]
        [SerializeField] private float maxYDistance;
        private float maxYDistanceSqrd;
        bool isOutsideYRange;

        void Start() {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            thisTransform = transform;
            thisTransform.position = playerTransform.position;
            playerGrounded = FindObjectOfType<Grounded>();

            maxYDistanceSqrd = maxYDistance * maxYDistance;
        }

        private void OnEnable() {
            PlayerHover.OnHoverStarted += SetFollowYPos;
            PlayerHover.OnHoverContinued += SetFollowYPos;
        }

        void LateUpdate() {
            if (isOutsideYRange) {
                if (Vector3.SqrMagnitude(playerTransform.position - thisTransform.position) <= 1) {
                    isOutsideYRange = false;
                }
                MoveToPosition(playerTransform.position);
            }
            else {
                if (Vector3.SqrMagnitude(playerTransform.position - thisTransform.position) > maxYDistanceSqrd ||
                    playerGrounded.IsOnGround) {
                    isOutsideYRange = true;
                    MoveToPosition(playerTransform.position);
                    return;
                }
                Vector3 targetPosition = playerTransform.position;
                if (Physics.Raycast(playerTransform.position, Vector3.down, out RaycastHit hit, 10f, playerGrounded.DetectableLayers)) {
                    //targetPosition.y = hit.transform.position.y + 0.2f;
                    targetPosition.y = hit.point.y + 0.25f;
                }
                else {
                    targetPosition.y = thisTransform.position.y;
                }
                MoveToPosition(targetPosition);
            }
        }

        private void SetFollowYPos() {
            isOutsideYRange = true;
        }

        private void MoveToPosition(Vector3 position) {
            float verticalSmoothness = followSmoothnessVertical;
            if (playerGrounded.IsOnGround) {
                verticalSmoothness = followSmoothnessVerticalGrounded;
            }
            float xPos = Mathf.Lerp(thisTransform.position.x, position.x, followSmoothnessHorizontal);
            float yPos = Mathf.Lerp(thisTransform.position.y, position.y, verticalSmoothness);
            float zPos = Mathf.Lerp(thisTransform.position.z, position.z, followSmoothnessHorizontal);
            thisTransform.position = new Vector3(xPos, yPos, zPos);
        }
    }
}
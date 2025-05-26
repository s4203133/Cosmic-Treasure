using UnityEngine;
using LMO;
using TMPro;

public class ParrotHoverScript : MonoBehaviour
{
    [Header("ACTIVATING ANIMATION")]
    [SerializeField] private GameObject parrot;
    [SerializeField] private GameObject playerObjectToParentTo;
    [SerializeField] private GameObject targetObjectToMoveTo;
    [SerializeField] private float startAnimationSpeed;

    [Header("FOLLOW SETTINGS")]
    [SerializeField] private GameObject playerObjectToFollow;
    [SerializeField] private float radiusToFollow;
    private Vector3 offset;
    private bool isFollowing;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector2 changeTargetPointTimerRange;
    private float changeTargetPointTimer;

    [SerializeField] private LayerMask groundLayers;

    private void Start() {
        isFollowing = true;
    }

    private void OnEnable() {
        PlayerHover.OnHoverStarted += StartHoverAnimation;
        PlayerHover.OnHoverContinued += StartHoverAnimation;
        PlayerHover.OnHoverEnded += StopHoverAnimation;
    }

    private void OnDisable() {
        PlayerHover.OnHoverStarted -= StartHoverAnimation;
        PlayerHover.OnHoverContinued -= StartHoverAnimation;
        PlayerHover.OnHoverEnded -= StopHoverAnimation;
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer() {
        parrot.transform.localScale = Vector3.one * 0.5f;
        if (isFollowing) {
            MoveToRandomPosition();
            Countdown();
        }
        else {
            MoveToPlayerHand();
        }
    }

    private void MoveToRandomPosition() {
        Vector3 targetPosition = playerObjectToFollow.transform.position + offset;
        PreventClippingThroughFloor(ref targetPosition);
        parrot.transform.position = Vector3.Lerp(parrot.transform.position, targetPosition, moveSpeed);
        parrot.transform.LookAt(targetPosition);
    }

    private void MoveToPlayerHand() {
        Vector3 targetPosition = targetObjectToMoveTo.transform.position + Vector3.up * 0.3f;
        parrot.transform.position = Vector3.Lerp(parrot.transform.position, targetPosition, startAnimationSpeed);
        parrot.transform.rotation = playerObjectToFollow.transform.rotation;
    }

    private void Countdown() {
        changeTargetPointTimer -= TimeValues.FixedDelta;
        if(changeTargetPointTimer <= 0) {
            changeTargetPointTimer = Random.Range(changeTargetPointTimerRange.x, changeTargetPointTimerRange.y);
            GetNewRandomPoint();
        }
    }

    private void GetNewRandomPoint() {
        offset = new Vector3(Random.Range(-radiusToFollow, radiusToFollow), 0.25f, Random.Range(-radiusToFollow, radiusToFollow));
        PreventOffsetClippingPlayer();
    }

    private void PreventClippingThroughFloor(ref Vector3 targetPosition) {
        RaycastHit hit;
        if(Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out hit, 1.25f, groundLayers)) {
            targetPosition = hit.point + (Vector3.up * 0.2f);
        }
    }

    private void PreventOffsetClippingPlayer() {
        if (offset.x > 0) {
            offset.x = Mathf.Max(offset.x, 1);
        }
        if (offset.z > 0) {
            offset.z = Mathf.Max(offset.z, 1);
        }

        if (offset.x < 0) {
            offset.x = Mathf.Min(-offset.x, -1);
        }
        if (offset.z < 0) {
            offset.z = Mathf.Min(-offset.z, -1);
        }
    }

    private void StartHoverAnimation() {
        parrot.transform.SetParent(playerObjectToParentTo.transform);
        isFollowing = false;
    }

    private void StopHoverAnimation() {
        parrot.transform.SetParent(null);
        isFollowing = true;
    }
}
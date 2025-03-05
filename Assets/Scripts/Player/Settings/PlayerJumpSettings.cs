using UnityEngine;

namespace LMO {

    [CreateAssetMenu(menuName = "Player/Player Jump Settings", fileName = "New Player Jump Settings")]
    public class PlayerJumpSettings : ScriptableObject {

        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] public float highJumpForce;
        [SerializeField] private float maxJumpDuration;
        [SerializeField] private float minJumpDuration;
        [SerializeField] private float minJumpInput;
        [Range(0.1f, 0.99f)]
        [SerializeField] private float jumpCutOffIntensity;

        [Header("Jump Apex")]
        [SerializeField] private float jumpApexThreshold;
        [SerializeField] private float jumpApexGravityReduction;
        [SerializeField] private float jumpApexMaxSpeed;
        [SerializeField] private float jumpApexSpeedIncrease;

        [Header("Falling")]
        [SerializeField] private float fallSpeed;
        [SerializeField] private float maxFallSpeed;

        // Pulic Accessors
        public float JumpForce => jumpForce;
        public float HighJumpForce => highJumpForce;
        public float MaxJumpDuration => maxJumpDuration;
        public float MinJumpDuration => minJumpDuration;
        public float MinJumpInput => minJumpInput;
        public float JumpCutOffIntensity => jumpCutOffIntensity;
        public float JumpApexThreshold => jumpApexThreshold;
        public float JumpApexGravityReduction => jumpApexGravityReduction;
        public float JumpApexMaxSpeed => jumpApexMaxSpeed;
        public float JumpApexSpeedIncrease => jumpApexSpeedIncrease;
        public float FallSpeed => fallSpeed;
        public float MaxFallSpeed => maxFallSpeed;
    }
}
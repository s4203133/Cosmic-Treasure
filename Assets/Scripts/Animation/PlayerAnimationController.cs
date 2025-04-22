using System;
using System.Collections;
using UnityEngine;

namespace LMO {

    public class PlayerAnimationController : MonoBehaviour {

        [SerializeField] private Animator animator;
        [SerializeField] private PlayerAnimationLayerHandler animationLayerHandling;
        private PlayerAnimations animations;

        private bool init;

        void Start() {
            Initialise();
        }

        private void OnEnable() {
            Initialise();
            SubscribeAnimationEvents();
        }

        private void OnDisable() {
            UnsubscribeAnimationEvents();
        }

        private void SubscribeAnimationEvents() {
            PlayerMovement.OnMoveStarted += animations.PlayRun;
            PlayerMovement.OnMoveStopped += animations.StopRun;
            PlayerMovement.OnKickBack += animations.PlayKickBack;
            PlayerMovement.OnKickBackEnded += animations.StopKickBack;
            PlayerJump.OnJump += animations.PlayJump;
            Grounded.OnLanded += animations.StopJump;
            Grounded.OnLanded += animations.StopDive;
            Grounded.OnLanded += animations.StopWallJump;
            Grounded.OnLanded += animations.StopWallSlide;
            PlayerSpinAttack.OnSpin += animations.PlaySpinAttack;
            PlayerSpinAttack.OnSpinEnd += animations.StopSpinAttack;
            PlayerHover.OnHoverStarted += animations.PlayHover;
            PlayerHover.OnHoverContinued += animations.PlayHover;
            PlayerHover.OnHoverEnded += animations.StopHover;
            InputHandler.jumpCancelled += animations.StopHover;
            PlayerDive.OnDive += animations.PlayDive;
            PlayerDive.OnHitObject += animations.StopDive;
            Grapple.OnGrappleStarted += animations.PlaySwing;
            Grapple.OnGrappleEnded+= animations.StopSwing;
            PlayerGroundPound.OnGroundPoundInitialised += animations.PlayGroundPound;
            Cannon.OnJumpingInCannon += animations.PlayCannonJump;
            PlayerWallJump.OnWallSlideStart += animations.PlayWallSlide;
            PlayerWallJump.OnWallSlideStart += animations.StopWallJump;
            PlayerWallJump.OnWallSlideEnd += animations.StopWallSlide;
            PlayerWallJump.OnWallJump += animations.PlayWallJump;
            PlayerWallJump.OnWallJump += animations.StopWallSlide;
            PlayerHealth.OnDamageTaken += animations.PlayKnockOver;
            PlayerHealth.OnStandingUp += animations.PlayStandUp;
            PlayerDeath.OnPlayerDied += animations.StopSwing;
        }

        private void UnsubscribeAnimationEvents() {
            PlayerMovement.OnMoveStarted -= animations.PlayRun;
            PlayerMovement.OnMoveStopped -= animations.StopRun;
            PlayerMovement.OnKickBack -= animations.PlayKickBack;
            PlayerMovement.OnKickBackEnded -= animations.StopKickBack;
            PlayerJump.OnJump -= animations.PlayJump;
            Grounded.OnLanded -= animations.StopJump;
            Grounded.OnLanded -= animations.StopDive;
            Grounded.OnLanded -= animations.StopWallJump;
            Grounded.OnLanded -= animations.StopWallSlide;
            PlayerSpinAttack.OnSpin -= animations.PlaySpinAttack;
            PlayerSpinAttack.OnSpinEnd -= animations.StopSpinAttack;
            PlayerHover.OnHoverStarted -= animations.PlayHover;
            PlayerHover.OnHoverContinued -= animations.PlayHover;
            PlayerHover.OnHoverEnded -= animations.StopHover;
            InputHandler.jumpCancelled -= animations.StopHover;
            PlayerDive.OnDive -= animations.PlayDive;
            PlayerDive.OnHitObject -= animations.StopDive;
            Grapple.OnGrappleStarted -= animations.PlaySwing;
            Grapple.OnGrappleEnded -= animations.StopSwing;
            PlayerGroundPound.OnGroundPoundInitialised -= animations.PlayGroundPound;
            Cannon.OnJumpingInCannon -= animations.PlayCannonJump;
            PlayerWallJump.OnWallSlideStart -= animations.PlayWallSlide;
            PlayerWallJump.OnWallSlideStart -= animations.StopWallJump;
            PlayerWallJump.OnWallSlideEnd -= animations.StopWallSlide;
            PlayerWallJump.OnWallJump -= animations.PlayWallJump;
            PlayerWallJump.OnWallJump -= animations.StopWallSlide;
            PlayerDeath.OnPlayerDied -= animations.StopSwing;
        }

        private void Initialise() {
            if (!init) {
                animationLayerHandling.Initialise(animator);
                animations = new PlayerAnimations();
                animations.Initialise(animator, animationLayerHandling);
                init = true;    
            }
        }
    }


    [Serializable]
    public class PlayerAnimationLayerHandler {

        private Animator animator;

        [SerializeField] private float activateLayerDuration;
        [SerializeField] private float deactivateLayerDuration;

        public int lowerBody { get; private set; }
        public int upperBody { get; private set; }

        public void Initialise(Animator playerAnimator) {
            animator = playerAnimator;
            lowerBody = animator.GetLayerIndex("LowerBody");
            upperBody = animator.GetLayerIndex("UpperBody");
        }

        public IEnumerator ActivateLayer(int layerIndex) {
            float t = 0;
            while (t < activateLayerDuration) {
                animator.SetLayerWeight(layerIndex, t);
                t += TimeValues.Delta;
                yield return null;
            }
            animator.SetLayerWeight(layerIndex, 1);
        }

        public IEnumerator DeactivateLayer(int layerIndex) {
            float t = deactivateLayerDuration;
            while (t > 0) {
                animator.SetLayerWeight(layerIndex, t);
                t -= TimeValues.Delta;
                yield return null;
            }
            animator.SetLayerWeight(layerIndex, 1);
        }
    }

    [Serializable]
    public class PlayerAnimations {
        private Animator animator;
        private PlayerAnimationLayerHandler layers;
        private PlayerAnimationParameters parameters;

        public void Initialise(Animator playerAnimator, PlayerAnimationLayerHandler layerHandler) {
            animator = playerAnimator;
            layers = layerHandler;
            parameters = new PlayerAnimationParameters(1);
        }

        public void PlayRun() => SetFloat(parameters.SPEED, 1);
        public void StopRun() => SetFloat(parameters.SPEED, 0);
        public void PlayJump() => SetBool(parameters.JUMP, true);
        public void StopJump() => SetBool(parameters.JUMP, false);
        public void PlaySpinAttack() => SetBool(parameters.SPIN, true);
        public void StopSpinAttack() => SetBool(parameters.SPIN, false);
        public void PlayHover() => SetBool(parameters.HOVER, true);
        public void StopHover() => SetBool(parameters.HOVER, false);
        public void PlayDive() => SetBool(parameters.DIVE, true);
        public void StopDive() => SetBool(parameters.DIVE, false);
        public void PlayWallSlide() => SetBool(parameters.WALL_SLIDE, true);
        public void StopWallSlide() => SetBool(parameters.WALL_SLIDE, false);
        public void PlayWallJump() => SetBool(parameters.WALL_JUMP, true);
        public void StopWallJump () => SetBool(parameters.WALL_JUMP, false);
        public void PlayKnockOver() => SetBool(parameters.KNOCKED_OVER, true);
        public void PlayStandUp() {
            SetBool(parameters.KNOCKED_OVER, false);
            SetBool(parameters.STAND_UP, true);
        }

        public void StopStandUp() => SetBool(parameters.STAND_UP, false);

        public void PlayGroundPound() => SetTrigger(parameters.GROUND_POUND);
        public void PlayCannonJump() => SetTrigger(parameters.CANNON_JUMP);
        public void PlayKickBack() => SetBool(parameters.KICK_BACK, true);
        public void StopKickBack() => SetBool(parameters.KICK_BACK, false);

        public void PlaySwing() {
            layers.ActivateLayer(layers.upperBody);
            SetBool(parameters.SWING, true);
        }
        public void StopSwing() { 
            layers.DeactivateLayer(layers.upperBody);
            SetBool(parameters.SWING, false); 
        }

        private void SetFloat(string name, int value) {
            animator.SetFloat(name, value);
        }
        private void SetBool(string name, bool value) {
            animator.SetBool(name, value);  
        }

        private void SetTrigger(string name) {
            animator.SetTrigger(name);
        }
    }

    public struct PlayerAnimationParameters {
        public string SPEED;
        public string JUMP;
        public string SPIN;
        public string HOVER;
        public string DIVE;
        public string SWING;
        public string GROUND_POUND;
        public string CANNON_JUMP;
        public string WALL_SLIDE;
        public string WALL_JUMP;
        public string KNOCKED_OVER;
        public string STAND_UP;
        public string KICK_BACK;

        public PlayerAnimationParameters(int id) {
            SPEED = "Speed";
            JUMP = "Jumping";
            SPIN = "SpinAttacking";
            HOVER = "Hovering";
            DIVE = "Diving";
            SWING = "Swinging";
            GROUND_POUND = "StartGroundPound";
            CANNON_JUMP = "CannonJump";
            WALL_SLIDE = "WallSlide";
            WALL_JUMP = "WallJump";
            KNOCKED_OVER = "KnockedOver";
            STAND_UP = "StandUp";
            KICK_BACK = "KickingBack";
        }
    }
}
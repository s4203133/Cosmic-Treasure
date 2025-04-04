using UnityEngine;

namespace LMO {

    public class TutorialStepInput : TutorialStep {

        [Space(15)]
        [SerializeField] private TutorialInputs targetAction;

        protected virtual void RegisterInput() {
            switch (targetAction) {
                case (TutorialInputs.Move):
                    PlayerMovement.OnMoveStarted += PerformLogic;
                    break;
                case (TutorialInputs.Jump):
                    PlayerJump.OnJump += PerformLogic;
                    break;
                case (TutorialInputs.Spin):
                    PlayerSpinAttack.OnSpin += PerformLogic;
                    break;
                case (TutorialInputs.Hover):
                    PlayerDive.OnDive += PerformLogic;
                    break;
                case (TutorialInputs.GroundPound):
                    PlayerGroundPound.OnGroundPoundFinished += PerformLogic;
                    break;
                case (TutorialInputs.Dive):
                    PlayerDive.OnDive += PerformLogic;
                    break;
                case (TutorialInputs.Grapple):
                    Grapple.OnGrappleStarted += PerformLogic;
                    break;
                case (TutorialInputs.Swing):
                    PlayerSwingState.OnSwingStart += PerformLogic;
                    break;
            }
        }

        protected virtual void UnregisterInput() {
            switch (targetAction) {
                case (TutorialInputs.Move):
                    PlayerMovement.OnMoveStarted -= PerformLogic;
                    break;
                case (TutorialInputs.Jump):
                    PlayerJump.OnJump -= PerformLogic;
                    break;
                case (TutorialInputs.Spin):
                    PlayerSpinAttack.OnSpin -= PerformLogic;
                    break;
                case (TutorialInputs.Hover):
                    PlayerDive.OnDive -= PerformLogic;
                    break;
                case (TutorialInputs.GroundPound):
                    PlayerGroundPound.OnGroundPoundFinished -= PerformLogic;
                    break;
                case (TutorialInputs.Dive):
                    PlayerDive.OnDive -= PerformLogic;
                    break;
                case (TutorialInputs.Grapple):
                    Grapple.OnGrappleStarted -= PerformLogic;
                    break;
                case (TutorialInputs.Swing):
                    PlayerSwingState.OnSwingStart -= PerformLogic;
                    break;
            }
        }

        public override void ActivateTutorialStep() {
            startActions.OnFinished += RegisterInput;
            startActions.StartSequence();
            completedActions.RegisterAsFinsishedStepSequence();
        }

        public override void DeactivateTutorialStep() {
            UnregisterInput();
        }

        protected override void PerformLogic() {
            startActions.OnFinished -= RegisterInput;
            completedActions.StartSequence();
        }
    }

    public enum TutorialInputs {
        Move,
        Jump,
        Spin,
        Hover,
        GroundPound,
        Dive,
        Grapple,
        Swing,
    }
}
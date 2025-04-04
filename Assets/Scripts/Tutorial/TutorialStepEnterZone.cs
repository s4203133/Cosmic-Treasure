using UnityEngine;

public class TutorialStepEnterZone : TutorialStep {

    [SerializeField] private TutorialTrigger targetCollider;

    public override void ActivateTutorialStep() {
        startActions.StartSequence();
        completedActions.RegisterAsFinsishedStepSequence();
        targetCollider.OnPlayerEnter += PerformLogic;
    }

    public override void DeactivateTutorialStep() {
        targetCollider.OnPlayerEnter -= PerformLogic;
    }

    protected override void PerformLogic() {
        completedActions.StartSequence();
    }
}

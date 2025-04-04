using UnityEngine;
using UnityEngine.Events;

public abstract class TutorialStep : MonoBehaviour
{
    [SerializeField] protected TutorialGuideSequence startActions;
    [SerializeField] protected TutorialGuideSequence completedActions;

    public abstract void ActivateTutorialStep();
    public abstract void DeactivateTutorialStep();
    protected abstract void PerformLogic();
}

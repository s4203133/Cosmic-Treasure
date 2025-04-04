using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TutorialGuideSequence
{
    [SerializeField] private UnityEvent[] sequence;
    private int currentAction;
    public static Action progressSequence;
    public static Action sequenceFinished;
    public Action OnFinished;

    private bool isFinishedStepSequence;

    public void StartSequence() {
        currentAction = 0;
        progressSequence += ProgressSequence;
        sequence[currentAction]?.Invoke();
    }

    private void ProgressSequence() {
        if(currentAction == sequence.Length - 1) {
            EndSequence();
            return;
        }
        currentAction++;
        sequence[currentAction]?.Invoke();
    }

    private void EndSequence() {
        if (isFinishedStepSequence) {
            sequenceFinished?.Invoke();
        }
        OnFinished?.Invoke();
        progressSequence -= ProgressSequence;
    }

    public void RegisterAsFinsishedStepSequence() {
        isFinishedStepSequence = true;
    }
}

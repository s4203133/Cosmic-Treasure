using System;
using UnityEngine;

public class TuturialManager : MonoBehaviour
{
    [SerializeField] private TutorialStep[] tutorialStages;
    private int currentStage;

    private void Start() {
        currentStage = 0;
        tutorialStages[0].ActivateTutorialStep();
    }

    private void OnEnable() {
        TutorialGuideSequence.sequenceFinished += ProgressTutorial;
    }

    private void OnDisable() {
        TutorialGuideSequence.sequenceFinished -= ProgressTutorial;
    }

    private void ProgressTutorial() {
        currentStage++;
        if (currentStage == tutorialStages.Length) {
            Debug.Log("Tutorial Finished!");
            return;
        }
        tutorialStages[currentStage].ActivateTutorialStep();
    }
}

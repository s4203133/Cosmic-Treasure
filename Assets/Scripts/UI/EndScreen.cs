using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private PlayersLevelStats levelStats;

    [Space(15)]
    [SerializeField] private Animator animator;

    [Space(15)]
    [SerializeField] private TextMeshProUGUI coinCounterText;
    [SerializeField] private TextMeshProUGUI timesDiedText;
    [SerializeField] private TextMeshProUGUI timeTookText;

    private void Start() {
        InitialiseLevelStatText();
    }

    public void Open() {
        animator.SetTrigger("Open");
    }

    private void InitialiseLevelStatText() {
        AssignCoinCounterText();
        AssignDeathsText();
        AssignTimeText();
    }

    private void AssignCoinCounterText() {
        if (coinCounterText != null) {
            coinCounterText.text = levelStats.CoinsCollected.ToString("000");
        }
    }

    private void AssignDeathsText() {
        if (timesDiedText != null) {
            timesDiedText.text = "x" + levelStats.TimesDied.ToString("00");
        }
    }

    private void AssignTimeText() {
        if (timeTookText != null) {
            int minutes = (int)(levelStats.TimeTook / 60);
            int seconds = (int)(levelStats.TimeTook % 60);
            timeTookText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}

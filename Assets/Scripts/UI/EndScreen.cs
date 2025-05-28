using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    //[SerializeField] private PlayersLevelStats levelStats;
    [SerializeField] private PlunderScore plunderScore;
    [SerializeField] private TextMeshProUGUI plunderScoreText;

    [Space(15)]
    [SerializeField] private Animator animator;
    //[SerializeField] private TextMeshProUGUI timesDiedText;
    //[SerializeField] private TextMeshProUGUI timeTookText;

    private void Start()
    {
        DisplayPlunderScore();
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }

    private void DisplayPlunderScore()
    {
        if (plunderScoreText != null)
        {
            plunderScore.CalculatePlunderScore();
            if (plunderScore.Score >= 100) {
                plunderScoreText.text = "100%";
            }
            else if (plunderScore.Score <= 0) {
                plunderScoreText.text = "00%";
            }
            else {
                plunderScoreText.text = plunderScore.Score.ToString("F2") + "%";
            }
        }
    }
}

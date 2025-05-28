using UnityEngine;
using TMPro;

public class RabbitCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public int totalRabbits = 3;
    private int collected = 0;

    public static RabbitCounter Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddRabbit()
    {
        collected++;
        UpdateUI();
    }

    void UpdateUI()
    {
        counterText.text = "Rabbits: " + collected + " / " + totalRabbits;
    }
}

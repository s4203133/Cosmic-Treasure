using UnityEngine;
using TMPro;
using System;

public class RabbitCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public int totalRabbits = 3;
    private int collected = 0;

    public static RabbitCounter Instance;

    public static Action OnCollectedRabbit;

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
        OnCollectedRabbit?.Invoke();
        UpdateUI();
    }

    void UpdateUI()
    {
        counterText.text = "Rabbits: " + collected + " / " + totalRabbits;
    }
}

using UnityEngine;

public class TorchController : MonoBehaviour
{
    public ParticleSystem flameEffect;
    public Color[] flameColors;
    private int currentColorIndex = 0;

    private void Start()
    {
        SetFlameColor(flameColors[currentColorIndex]);
    }

    public void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % flameColors.Length;
        SetFlameColor(flameColors[currentColorIndex]);
        PuzzleManager.Instance.CheckCombination();
    }

    void SetFlameColor(Color color)
    {
        var main = flameEffect.main;
        main.startColor = color;
    }

    public int GetColorIndex()
    {
        return currentColorIndex;
    }
}

using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public TorchController[] torches;
    public int[] correctPattern;
    public GameObject rabbitCage;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckCombination()
    {
        for (int i = 0; i < torches.Length; i++)
        {
            if (torches[i].GetColorIndex() != correctPattern[i])
                return;
        }

        Debug.Log("Puzzle Solved!");
        rabbitCage.SetActive(false); // Or trigger animation here
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Camera Index Data")]
public class SceneIndexData : ScriptableObject
{
    [SerializeField] private int endLevelScene;
    public int EndLevelScene => endLevelScene;
}

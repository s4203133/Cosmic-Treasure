using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Global Event Manager", fileName = "New Global Event Manager")]
public class GlobalEventManager : ScriptableObject
{
    public delegate void CustomEvent();
    public delegate void CustomEventF1(float param1);

    public static CustomEvent SceneRestarted;

    public static void OnSceneRestarted() {
        SceneRestarted?.Invoke();
    }
}

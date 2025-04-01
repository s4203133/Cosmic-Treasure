using UnityEngine;

public class BottleMessage : MonoBehaviour
{
    [TextArea(4, 6)]
    [SerializeField] private string message;
    public string Message => message;
}

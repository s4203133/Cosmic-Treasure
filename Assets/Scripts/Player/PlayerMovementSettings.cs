using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Movement Settings", fileName = "New Player Movement Settings")]
public class PlayerMovementSettings : ScriptableObject
{
    [Header("MOVEMENT SETTINGS")]
    [SerializeField] private float maxSpeed;
    public float MaxSpeed => maxSpeed;

    [Space(15)]
    [SerializeField] protected float rotateSpeed;
    public float RotationSpeed => rotateSpeed;

    [Space(15)]
    [SerializeField] private bool canChangeDirectionQuickly;
    public bool CanChangeDirectionQuickly => canChangeDirectionQuickly;
    [Range(0.1f, 1f)]
    [SerializeField] protected float changeDirectionThreshhold;
    public float ChangeDirectionThreshhold => changeDirectionThreshhold;

    [Space(15)]
    [SerializeField] private MotionCurve acceleration;
    public MotionCurve Acceleration => acceleration;
    [SerializeField] private MotionCurve deceleration;
    public MotionCurve Deceleration => deceleration;

    protected Vector3 velocity;
    protected Vector3 moveDirection;
}

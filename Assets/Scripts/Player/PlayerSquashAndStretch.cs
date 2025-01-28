using UnityEngine;

public class PlayerSquashAndStretch : MonoBehaviour
{
    [SerializeField] private SquashAndStretch jump;
    [SerializeField] private SquashAndStretch land;
    [SerializeField] private SquashAndStretch spinAttack;
    [SerializeField] private SquashAndStretch groundPound;

    public SquashAndStretch Jump => jump;
    public SquashAndStretch Land => land;
    public SquashAndStretch SpinAttack => spinAttack;
    public SquashAndStretch GroundPound => groundPound;
}

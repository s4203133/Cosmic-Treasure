using UnityEngine;

namespace LMO.Player {

    public class PlayerSquashAndStretch : MonoBehaviour {

        [SerializeField] private SquashAndStretch jump;
        [SerializeField] private SquashAndStretch highJump;
        [SerializeField] private SquashAndStretch land;
        [SerializeField] private SquashAndStretch spinAttack;
        [SerializeField] private SquashAndStretch groundPound;
        [SerializeField] private SquashAndStretch dive;

        public SquashAndStretch Jump => jump;
        public SquashAndStretch HighJump => highJump;
        public SquashAndStretch Land => land;
        public SquashAndStretch SpinAttack => spinAttack;
        public SquashAndStretch GroundPound => groundPound;
        public SquashAndStretch Dive => dive;
    }
}
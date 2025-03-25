
namespace LMO {

    public class SwingJoint : GrapplePoint {
        protected override void Start() {
            base.Start();
            disconnectWhenPlayerFalls = false;
        }
    }
}
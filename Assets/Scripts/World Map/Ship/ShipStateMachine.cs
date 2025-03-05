
namespace LMO {

    public class ShipStateMachine : PlayerStateMachine {

        public ShipIdle shipIdleState;
        public ShipMove shipMoveState;

        protected override void InitialiseStates() {
            shipIdleState = new ShipIdle(controller);
            shipMoveState = new ShipMove(controller);
        }

        protected override void StartStateMachine() {
            currentState = shipIdleState;
            stateName = currentState.ToString();
            currentState.OnStateEnter();
        }
    }
}
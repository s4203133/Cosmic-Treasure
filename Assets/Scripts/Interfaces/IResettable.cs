
namespace LMO {

    /// <summary>
    /// Interface for objects that need to reset data or behaviour when the player dies and respawns
    /// </summary>
    public interface IResettable {
        public void Reset();

        public void Enable() {
            PlayerDeath.OnPlayerDied += Reset;
        }

        public void Disable() {
            PlayerDeath.OnPlayerDied -= Reset;
        }
    }
}
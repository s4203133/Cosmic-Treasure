namespace NR {
    /// <summary>
    /// Interface for objects that have behaviour that is called when shot by a projectile.
    /// </summary>
    public interface IShootable {
        public void OnShot(Projectile projectile);
    }
}
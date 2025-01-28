public interface IBreakable
{
    public delegate void BreakEvent();
    public static BreakEvent OnBroken;

    public void Break();
}

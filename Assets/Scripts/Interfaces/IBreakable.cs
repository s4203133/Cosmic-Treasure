using System;

public interface IBreakable
{
    public static Action OnBroken;

    public void Break();
}

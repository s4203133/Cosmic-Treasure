using System;

namespace LMO.Interfaces {
    public interface IBreakable {
        public static Action OnBroken;

        public void Break();
    }
}
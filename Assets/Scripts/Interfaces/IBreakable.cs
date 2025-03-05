using System;

namespace LMO {
    public interface IBreakable {
        public static Action OnBroken;

        public void Break();
    }
}
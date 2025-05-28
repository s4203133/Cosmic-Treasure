using System;

namespace LMO {

    public class Coin : Collectible {
        public static Action OnCollectedCoin;

        protected override void Collected() {
            OnCollectedCoin?.Invoke();
        }
    }
}
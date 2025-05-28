
using System;

namespace LMO {

    public class Gem : Collectible {
        public static Action OnGemCollected;

        protected override void Collected() {
            OnGemCollected?.Invoke();
        }
    }
}
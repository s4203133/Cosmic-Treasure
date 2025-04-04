
namespace LMO {

    public class CoinDisplay : UIDisplay {
        protected override void SubscribeEvents() {
            Coin.OnCollected += OpenDisplay;
        }

        protected override void UnsubscribeEvents() {
            Coin.OnCollected -= OpenDisplay;
        }
    }
}
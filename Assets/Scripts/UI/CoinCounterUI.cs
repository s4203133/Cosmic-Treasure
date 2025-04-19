
namespace LMO {
    public class CoinCounterUI : CounterUI {

        protected override void UpdateText() {
            counterText.text = counter.value.ToString("000");
        }

        protected override void SubscribeToEvent() {
            Coin.OnCollected += UpdateText;
        }

        protected override void UnsubscribeFromEvent() {
            Coin.OnCollected -= UpdateText;
        }
    }
}
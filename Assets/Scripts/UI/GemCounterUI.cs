
namespace LMO {

    public class GemCounter : CounterUI {

        protected override void UpdateText() {
            counterext.text = counter.value.ToString("") + "/3";
        }

        protected override void SubscribeToEvent() {
            Gem.OnCollected += UpdateText;
        }

        protected override void UnsubscribeFromEvent() {
            Gem.OnCollected -= UpdateText;
        }
    }
}
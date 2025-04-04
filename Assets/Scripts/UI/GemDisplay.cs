namespace LMO {

    public class GemDisplay : UIDisplay {
        protected override void SubscribeEvents() {
            Gem.OnCollected += OpenDisplay;
        }

        protected override void UnsubscribeEvents() {
            Gem.OnCollected -= OpenDisplay;
        }
    }
}
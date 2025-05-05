using NR;

namespace LMO {

    public class GemCounter : CounterUI {
        //<NR>
        private int maxGems;

        public void SetMaxGems(int max) {
            maxGems = max;
        }

        protected override void UpdateText() {
            counterText.text = $"{LevelSaveManager.Instance.GetLevelGems()}/{maxGems}";
        }
        //</NR>

        protected override void SubscribeToEvent() {
            Gem.OnCollected += UpdateText;
        }

        protected override void UnsubscribeFromEvent() {
            Gem.OnCollected -= UpdateText;
        }
    }
}
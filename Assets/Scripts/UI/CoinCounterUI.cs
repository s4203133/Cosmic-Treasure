using TMPro;
using UnityEngine;

namespace LMO {

    public class CoinCounterUI : MonoBehaviour {
        private TextMeshProUGUI coinText;
        [SerializeField] private FloatVariable cointCounter;

        private void Awake() {
            coinText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable() => Coin.OnCoinCollected += UpdateText;

        private void OnDisable() => Coin.OnCoinCollected -= UpdateText;

        private void UpdateText() {
            coinText.text = cointCounter.value.ToString("000");
        }

        private void OnApplicationQuit() {
            cointCounter.value = 0;
        }
    }
}
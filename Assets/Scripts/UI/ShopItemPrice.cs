using NR;
using TMPro;
using UnityEngine;

public class ShopItemPrice : MonoBehaviour
{
    [SerializeField] private string textToDisplayOnBrought;
    [SerializeField] private PlayerOutfitItem item;
    private TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void NotifyBrought() {
        if (item.purchased) {
            text.text = textToDisplayOnBrought;
        }
    }
}

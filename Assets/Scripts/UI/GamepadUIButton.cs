using LMO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamepadUIButton : MonoBehaviour
{
    [SerializeField] private bool startHighlighted;
    private Color unHighlightedColour;
    [SerializeField] private Color highlightedColour;
    private bool isHighlighted;

    [SerializeField] private GamepadUIButton leftButton;
    [SerializeField] private GamepadUIButton rightButton;
    [SerializeField] private GamepadUIButton upButton;
    [SerializeField] private GamepadUIButton downButton;

    [Space(10)]
    [SerializeField] private UnityEvent OnHighlighted;

    private Image image;
    private Button button;

    private bool initialised;

    private void Start() {
        Initialise();

        if (startHighlighted) {
            Highlight();
        }
    }

    private void Initialise() {
        if (!initialised) {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
            unHighlightedColour = image.color;
            initialised = true;
        }
    }

    private void OnDisable() {
        DeactivateInput();
    }

    private void OnDestroy() {
        DeactivateInput();
    }

    private void ActivateInput() {
        InputHandler.Left += HighlightLeftButton;
        InputHandler.Right += HighlightRightButton;
        InputHandler.Up += HighlightUpButton;
        InputHandler.Down += HighlightDownButton;
        InputHandler.click += SelectButton;
    }

    private void DeactivateInput() {
        InputHandler.Left -= HighlightLeftButton;
        InputHandler.Right -= HighlightRightButton;
        InputHandler.Up -= HighlightUpButton;
        InputHandler.Down -= HighlightDownButton;
        InputHandler.click -= SelectButton;
    }

    public void SelectButton() {
        if (!gameObject.activeInHierarchy) {
            return;
        }
        if (isHighlighted) {
            button.onClick?.Invoke();
        }
    }

    public void Highlight() {
        Initialise();
        image.color = highlightedColour;
        isHighlighted = true;
        OnHighlighted?.Invoke();
        ActivateInput();
    }

    public void UnHighlight() {
        Initialise();
        image.color = unHighlightedColour;
        isHighlighted = false;
        DeactivateInput();
    }

    private void HighlightButton(GamepadUIButton button) {
        if(button == null || !isHighlighted || !gameObject.activeInHierarchy) {
            return;
        }
        UnHighlight();
        button.Highlight();
    }

    private void HighlightLeftButton() => HighlightButton(leftButton);
    private void HighlightRightButton() => HighlightButton(rightButton);
    private void HighlightUpButton() => HighlightButton(upButton);
    private void HighlightDownButton() => HighlightButton(downButton);
}

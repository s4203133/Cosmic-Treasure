using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Image cursor;
    public Transform ResolutionTypes, Menu;
    public PlayerInput input;
    //private InputAction clicked;
   
   

    public VirtualMouseInput MouseInput;
    private int fullscreen = 1;
    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 0;
        input.SwitchCurrentActionMap("UI");
        //clicked = input.actions.FindAction("click");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayFirstLevel() {
        Time.timeScale = 1;        
        input.SwitchCurrentActionMap("Movement");
        SceneManager.LoadScene("First_Level");
    }
    public void PlayTutorialLevel()
    {
        Time.timeScale = 1;
        input.SwitchCurrentActionMap("Movement");
        SceneManager.LoadScene("Turtorial_Hub_Level");
    }
    public void BackMainMenu()
    {
        Time.timeScale = 1;
        input.SwitchCurrentActionMap("Movement");
        SceneManager.LoadScene("Main Menu");
    }
    public void FullScreenButton() {
        fullscreen += 1;
        if (fullscreen == 1) {
            Screen.fullScreen = true;
        }
        if (fullscreen == 2) {
            Screen.fullScreen = false;
        }
        if (fullscreen == 3) {
            fullscreen = 1;
        }
       
    }

    public void QuitClicked() {
        Application.Quit();
    }
    public void ResolutionOptions() {
        Menu.gameObject.SetActive(false);
        ResolutionTypes.gameObject.SetActive(true);
    }
    public void LevelSelector()
    {
        Menu.gameObject.SetActive(false);
        ResolutionTypes.gameObject.SetActive(true);
    }
    public void Back() {
        Menu.gameObject.SetActive(true);
        ResolutionTypes.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update() {
        //point = input.actions.FindAction("click");
        //Debug.Log(MouseInput.virtualMouse.position.ReadValue());

        //if (point.IsPressed()) {
        //    Debug.Log("pressed");
        //}
        
    }
}

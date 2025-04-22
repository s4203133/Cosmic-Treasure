using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public Button Playbutton;
    public Button Quitbutton;
    public PlayerInput input;
    private InputAction pause;
    // Start is called before the first frame update
    void Start()
    {
        pause = input.actions.FindAction("Pause");
        Playbutton.gameObject.SetActive(false);
        Quitbutton.gameObject.SetActive(false);
    }
    //private void OnEnable() {
    //    pause.Enable();
        
    //}
    //private void OnDisable() {
    //    pause.Disable();
        
    //}

    public void PlayClicked() {
        Time.timeScale = 1;
        input.SwitchCurrentActionMap("Movement");
        Playbutton.gameObject.SetActive(false);
        Quitbutton.gameObject.SetActive(false);
    }
    public void QuitClicked() {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (pause.IsPressed()) {
            Time.timeScale = 0;
            Playbutton.transform.gameObject.SetActive(true);
            Quitbutton.gameObject.SetActive(true);
            input.SwitchCurrentActionMap("UI");
        }
        
    }
}

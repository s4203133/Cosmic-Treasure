using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void ContinueButton()
    {
        SceneManager.LoadScene(1); //Loading the current tutorial level 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

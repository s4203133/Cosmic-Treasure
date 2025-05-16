using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Restypes : MonoBehaviour
{

    public Resolution res;
    public int resHeight;
    public int resWidth;
    // Start is called before the first frame update
    void Start()
    {
        res = new Resolution();
        res.height = resHeight;
        res.width = resWidth;
    }
    public void Resolutions() {
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log(new Vector2(res.width, res.height));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

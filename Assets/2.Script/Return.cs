using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    

   
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartScene");
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}

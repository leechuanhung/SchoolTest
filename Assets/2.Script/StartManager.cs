using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    

    void Start()
    {
        Invoke("StartScene", 5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("MainScene");
        }
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
    private void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

}

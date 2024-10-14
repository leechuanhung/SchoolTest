using UnityEngine;
using UnityEngine.SceneManagement;

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
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }


}

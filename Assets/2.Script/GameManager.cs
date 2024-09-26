using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Player player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIStage;
    public TextMeshProUGUI remaindertime;

    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        //Change Stage
        if (stageIndex < Stages.Length)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        {
            //Player Contl Lock
            Time.timeScale = 0;
            //Restart UI
            Debug.Log("게임 클리어");
        }

        //Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(0, 0, 0);
        }
        else
        {
            //Player Die Effect
            player.OnDie();
            //Result UI
            SceneManager.LoadScene("OverScene");
            BackStart();

        }
    }

    void BackStart()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Health Down
        HealthDown();

        //Player Reposition
        if (health > 1)
        {
            PlayerReposition();

        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
}

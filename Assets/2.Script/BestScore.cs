using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // �ְ� ������ ǥ���� UI �ؽ�Ʈ
    private int currentScore = 0; // ���� ����
    private int highScore = 0; // �ְ� ����

    void Start()
    {
        // ����� �ְ� ���� �ε�
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

   
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        currentScore += score;

        // �ְ� ���� ���� üũ
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore); // �ְ� ���� ����
        }

        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "�ְ� ����: " + highScore.ToString();
    }

    // ���� ���� �� ȣ���Ͽ� ���� (���� ����)
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}

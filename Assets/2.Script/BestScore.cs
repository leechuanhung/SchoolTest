using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // 최고 점수를 표시할 UI 텍스트
    private int currentScore = 0; // 현재 점수
    private int highScore = 0; // 최고 점수

    void Start()
    {
        // 저장된 최고 점수 로드
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

   
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        currentScore += score;

        // 최고 점수 갱신 체크
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore); // 최고 점수 저장
        }

        UpdateHighScoreText();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "최고 점수: " + highScore.ToString();
    }

    // 게임 종료 시 호출하여 저장 (선택 사항)
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}

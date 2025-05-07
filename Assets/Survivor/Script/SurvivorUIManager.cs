using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SurvivorUIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public string reStartScene;
    public string changeScene;



    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void HighScore(int highScore)
    {
        highScoreText.text = highScore.ToString();
    }

    // 재시작
    public void ReStartScene()
    {
        SceneManager.LoadScene(reStartScene);
    }

    // 메타버스 씬 호출
    public void ChangeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(changeScene);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneGameManager : MonoBehaviour
{
    static PlaneGameManager gameManager;

    public static PlaneGameManager Instance
    {
        get { return gameManager; }
    }

    private int currentScore = 0;
    public int planeHighScore = 0;
    public GameObject panel;
    PlaneUIManager uiManager;

    public PlaneUIManager UIManager
    {
        get { return uiManager; }
    }
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<PlaneUIManager>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
        panel.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        LoadHighScore();
        if (currentScore > planeHighScore)
        {
            planeHighScore = currentScore;
            SaveHighScore();

        }
        UIManager.HighScore(planeHighScore);
        panel.SetActive(true);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);

    }

    void LoadHighScore()
    {
        planeHighScore = PlayerPrefs.GetInt("PlaneHighScore", 0);
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("PlaneHighScore", planeHighScore);
        PlayerPrefs.Save();
    }

}
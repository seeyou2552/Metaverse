using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivorGameManager : MonoBehaviour
{
    static SurvivorGameManager gameManager;
    public GameObject target;
    public GameObject gameOverPanel;

    public static SurvivorGameManager Instance
    {
        get { return gameManager; }
    }

    public int currentScore = 0;
    public int survivorHighScore = 0;

    SurvivorUIManager uiManager;
    public GameObject easyMonster;
    public GameObject normalMonster;
    public GameObject hardMonster;
    public GameObject expertMonster;
    public GameObject hellMonster;
    private float timer = 0;
    public float spawnInterval = 10f;
    public float spawnRadius = 10f;
    public int monsterCount = 4;

    public SurvivorUIManager UIManager
    {
        get { return uiManager; }
    }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<SurvivorUIManager>();
    }

    private IEnumerator Start()
    {
        while (target == null)
        {
            if (target == null)
            {
                target = GameObject.FindWithTag("Player");
            }
            yield return null;

        }
        uiManager.UpdateScore(0);
        SpawnMonsters();
        yield return null;
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMonsters();
            timer = 0f;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        LoadHighScore();
        if (currentScore > survivorHighScore)
        {
            survivorHighScore = currentScore;
            SaveHighScore();

        }
        UIManager.HighScore(survivorHighScore);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);

    }

    void SpawnMonsters()
    {
        if (monsterCount <= 25) SpawnEasyMonster();
        if (monsterCount >= 10 && monsterCount <= 40) SpawnNormalMonster();
        if (monsterCount >= 20) SpawnHardMonster();
        if (monsterCount >= 35) SpawnExpertMonster();
        if (monsterCount >= 50) SpawnHellMonster();
        monsterCount += 2;
    }

    void SpawnEasyMonster()
    {
        for (int i = 0; i < monsterCount; i++)
        {
            Vector2 randomPos = (Vector2)target.transform.position + UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(easyMonster, randomPos, Quaternion.identity);
        }
    }

    void SpawnNormalMonster()
    {
        for (int i = 0; i < monsterCount - 8; i++)
        {
            Vector2 randomPos = (Vector2)target.transform.position + UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(normalMonster, randomPos, Quaternion.identity);
        }
    }

    void SpawnHardMonster()
    {
        for (int i = 0; i < monsterCount - 18; i++)
        {
            Vector2 randomPos = (Vector2)target.transform.position + UnityEngine.Random.insideUnitCircle.normalized * (spawnRadius + 5);
            Instantiate(hardMonster, randomPos, Quaternion.identity);
        }
    }

    void SpawnExpertMonster()
    {
        for (int i = 0; i < monsterCount - 30; i++)
        {
            Vector2 randomPos = (Vector2)target.transform.position + UnityEngine.Random.insideUnitCircle.normalized * (spawnRadius + 7);
            Instantiate(expertMonster, randomPos, Quaternion.identity);
        }
    }

    void SpawnHellMonster()
    {
        for (int i = 0; i < monsterCount - 50; i++)
        {
            Vector2 randomPos = (Vector2)target.transform.position + UnityEngine.Random.insideUnitCircle.normalized * (spawnRadius + 10);
            Instantiate(hellMonster, randomPos, Quaternion.identity);
        }
    }



    void LoadHighScore()
    {
        survivorHighScore = PlayerPrefs.GetInt("SurvivorHighScore", 0);
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("SurvivorHighScore", survivorHighScore);
        PlayerPrefs.Save();
    }

}
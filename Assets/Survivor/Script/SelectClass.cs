using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectClass : MonoBehaviour
{
    public bool isMagicKnight = false;
    public GameObject magicKnight;
    public bool isArcher = false;
    public GameObject archer;
    public bool isThief = false;
    public GameObject thief;
    public GameObject classPanel;



    void Start()
    {
        Time.timeScale = 0f;
    }

    public void IsMagicKnight()
    {
        Vector2 vector = new Vector2(0f,0f);
        isMagicKnight = true;
        Instantiate(magicKnight, vector, Quaternion.identity);
        classPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IsArcher()
    {
        Vector2 vector = new Vector2(0f,0f);
        isArcher = true;
        Instantiate(archer, vector, Quaternion.identity);
        classPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IsThief()
    {
        Vector2 vector = new Vector2(0f,0f);
        isThief = true;
        Instantiate(thief, vector, Quaternion.identity);
        classPanel.SetActive(false);
        Time.timeScale = 1f;
        
    }
    
}

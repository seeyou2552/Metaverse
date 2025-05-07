using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int highScore;
    public TMP_Text score;
    public TMP_Text gameName;
    public GameObject scoreBoard;
    public GameObject showScoreBoardBtn;
    public Animator musicAnimator;

    void Start()
    {
        OnPlainBtn();
    }

    void LoadPlaneScore()
    {
        highScore = PlayerPrefs.GetInt("PlaneHighScore", 0);
        gameName.text = "Plane";
        score.text = highScore.ToString();
    }

    void LoadSurvivorScore()
    {
        highScore = PlayerPrefs.GetInt("SurvivorHighScore", 0);
        gameName.text = "Survivor";
        score.text = highScore.ToString();
    }

    public void OnPlainBtn()
    {
        LoadPlaneScore();
    }

    public void OnSurvivorBtn()
    {
        LoadSurvivorScore();
    }



    public void HideScoreBoardBtn()
    {
        scoreBoard.SetActive(false);
        showScoreBoardBtn.SetActive(true);
    }

    public void ShowScoreBoardBtn()
    {
        scoreBoard.SetActive(true);
        showScoreBoardBtn.SetActive(false);
    }

    public void ShowMusicBox()
    {
        musicAnimator.SetBool("isOn", true);
    }
    public void HideMusicBox()
    {
        musicAnimator.SetBool("isOn", false);
    }

}

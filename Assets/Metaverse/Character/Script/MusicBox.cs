using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canTrigger = false;
    public AudioClip[] musicList;
    private AudioSource audio;
    UIManager uiManager;
    void Start()
    {
        GameObject obj = GameObject.Find("UIManager");
        uiManager = obj.GetComponent<UIManager>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canTrigger) MusicTrigger();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canTrigger = false;
        }
    }

    void MusicTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            uiManager.ShowMusicBox();
        }
    }

    public void SetMusic(int index)
    {
        if (index >= 0 && index < musicList.Length)
        {
            audio.Stop();
            audio.clip = musicList[index];
            audio.loop = true;
            audio.volume = 0.3f;
            audio.Play();
        }
    }
}

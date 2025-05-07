using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public Sprite openDoor;
    public Sprite closeDoor;
    SpriteRenderer renderer;
    Collider2D collider;
    public bool doorControll = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if(doorControll) DoorTrigger();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorControll = true;
        }
    }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         doorControll = false;
    //     }
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doorControll = true;
        }
    }

    void DoorTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isOpen)
        {
            renderer.sprite = openDoor;
            collider.isTrigger = true;
            doorControll = false;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && isOpen)
        {
            renderer.sprite = closeDoor;
            collider.isTrigger = false;
            doorControll = false;
            isOpen = false;
        }
    }

}

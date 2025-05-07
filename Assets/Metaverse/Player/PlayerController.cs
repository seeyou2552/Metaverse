using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer render;
    protected AnimationHandler animationHandler;
    // public NPCManager npc;
    public bool isTalk = false;
    public GameObject dialogueArea;
    public DialogManager dialog;

    void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        if (!dialogueArea.activeSelf) PlayerMove();
        else
        {
            rigid.velocity = new Vector2(0,0);
        }
        
        if(dialog != null && !dialog.isInputBlocked)PlayerDialog();
        else return;
    }

    public void PlayerMove()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left * 10;
            render.flipX = true;

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right * 10;
            render.flipX = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up * 10;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down * 10;
        }

        rigid.velocity = new Vector2(direction.x, direction.y);
        animationHandler.Move(direction);
    }

    public void PlayerDialog()
    {

        if (Input.GetKeyDown(KeyCode.Z) && isTalk == true)
        {
            // npc.StartDialogue();
            dialog.StartDialog();        
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("NPC"))
        {
            // npc = collider.GetComponent<NPCManager>();
            dialog = collider.GetComponent<DialogManager>();
            isTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("NPC"))
        {
            // npc = null;
            dialog = null;;
            isTalk = false;
        }
    }
}

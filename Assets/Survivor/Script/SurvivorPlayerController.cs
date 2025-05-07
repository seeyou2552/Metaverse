using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorPlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer render;
    protected AnimationHandler animationHandler;
    private SurvivorPlayer player;
    private Camera mainCamera;

    void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        player = GetComponent<SurvivorPlayer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        PlayerMove();
        if (mouseWorldPos.x < transform.position.x)
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }

        

    }
    private void PlayerMove()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left * player.speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right * player.speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up * player.speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down * player.speed;
        }

        rigid.velocity = new Vector2(direction.x, direction.y);
        animationHandler.Move(direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorPlayerManager : MonoBehaviour
{
    private SurvivorPlayer player;
    private Enemy enemy;
    public Animator animator;
    private SpriteRenderer renderer;

    SelectClass selectClass;

    void Awake()
    {
        player = GetComponent<SurvivorPlayer>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GameObject obj = GameObject.Find("UIManager");
        selectClass = obj.GetComponent<SelectClass>();

        if(selectClass.isArcher)
        {
            player.speed = 6;
            player.pow = 5;
        }
        else if(selectClass.isThief)
        {
            player.speed = 7;
            player.pow = 4;
        }
        else if(selectClass.isMagicKnight)
        {
            player.speed = 5;
            player.pow = 3;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnDamage(other);
        }

        if(other.CompareTag("ExpPoint"))
        {
            GetExp();
            Destroy(other.gameObject);
        }
    }

    void OnDamage(Collider2D other)
    {
        enemy = other.GetComponent<Enemy>();
        animator.SetBool("IsHit", true);
        player.hp -= enemy.pow;

        if(player.hp <= 0)
        {
            SurvivorGameManager gameManager = FindObjectOfType<SurvivorGameManager>();
            gameManager.GameOver();
        }

        gameObject.layer = LayerMask.NameToLayer("DamagedPlayer");
        renderer.color = new Color(1, 1, 1, 0.4f);
        StartCoroutine(OnInvincibility());
    }

    IEnumerator OnInvincibility()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = LayerMask.NameToLayer("Player");
        animator.SetBool("IsHit", false);
        renderer.color = new Color(1, 1, 1, 1);
    }

    void GetExp()
    {
        player.exp++;
        if(player.exp >= player.maxExp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        player.level++;
        player.exp -= player.maxExp;
        player.maxExp *= 2;
        player.pow += 2;
    }
}

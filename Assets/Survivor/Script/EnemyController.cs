using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    private SpriteRenderer spriteRenderer;
    public GameObject expPoint;
    private Enemy enemy;
    private WeaponController weapon;

    public SurvivorUIManager uiManager;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
        target = GameObject.FindWithTag("Player");
        GameObject uiObj = GameObject.Find("UIManager");
        uiManager = uiObj.GetComponent<SurvivorUIManager>();

    }

    void Update()
    {
        EnemyMove();

        if (enemy.hp <= 0)
        {
            DeadEnemy();
        }
    }

    void EnemyMove()
    {
        Vector2 direction = Vector2.zero;

        direction = (target.transform.position - transform.position).normalized;

        transform.position += (Vector3)(direction * enemy.speed * Time.deltaTime);

        if (direction.x < 0) this.spriteRenderer.flipX = true;
        else if (direction.x > 0) this.spriteRenderer.flipX = false;
    }

    void DeadEnemy()
    {
        if (uiManager == null) return;
            SurvivorGameManager.Instance.AddScore(enemy.point);
        Instantiate(expPoint, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.GameObject());
    }
}
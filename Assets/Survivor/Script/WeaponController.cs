using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<GameObject> swordList;
    public GameObject defaultSwordPrefab;
    public GameObject macePrefab;
    public GameObject spearPrefab;
    public GameObject defaultShotPrefab;
    public SurvivorPlayer player;
    public SelectClass selectClass;
    public float shootInterval = 2f;
    public float spawnDistance = 1.5f;
    private float timer;

    void Start()
    {
        GameObject obj = GameObject.Find("UIManager");
        selectClass = obj.GetComponent<SelectClass>();

        // Magic Knight 리스트 생성
        if (selectClass.isMagicKnight)
        {
            swordList.AddRange(new List<GameObject> { defaultSwordPrefab, macePrefab, spearPrefab  });
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        Attack(); // 자동 공격
    }

    void Attack()
    {
        if (timer >= shootInterval)
        {

            if (selectClass.isArcher || selectClass.isThief)
            {
                ShotWeapon();
            }

            if (selectClass.isMagicKnight)
            {
                CreateWeapon();
            }

            timer = 0f;
        }
    }

    void CreateWeapon()
    {

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // 현재 위치에서 마우스 방향 계산
        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        // 생성 위치 = 현재 위치 + 방향 * 거리
        Vector3 spawnPos = transform.position + direction * spawnDistance;

        // 회전각 계산 (왼쪽을 기준으로 회전)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 원래 애니메이션이 왼쪽 기준이면 180도 빼줘야 왼쪽이 forward가 됨
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        for (int i = 0; i < player.level; i++)
        {
            if (i >= swordList.Count) return;
            Instantiate(swordList[i], spawnPos, rotation);
        }
    }

    // Archer, Thief 전용
    void ShotWeapon()
    {
        // 마우스 위치 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // 발사체 생성
        Vector2 direction = (new Vector2(mousePos.x, mousePos.y) - (Vector2)transform.position).normalized;

        // int projectileCount = Mathf.Max(player.level, 1); // 최소 1개 보장

        float fullAngle = (player.level > 1) ? Mathf.Clamp(15f * (player.level - 1), 0f, 90f) : 0f;
        float startAngle = -fullAngle / 2f;

        for (int i = 0; i < player.level; i++)
        {
            // 퍼지는 각도 계산 (레벨 1일 경우 0도)
            float angleOffset = (player.level > 1) ? startAngle + (fullAngle / (player.level - 1)) * i : 0f;

            // 회전된 방향 벡터 계산
            float angleInRad = Mathf.Atan2(direction.y, direction.x) + angleOffset * Mathf.Deg2Rad;
            Vector2 rotatedDirection = new Vector2(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad)).normalized;

            // 시각적 회전을 위한 Z 회전값 계산
            float zRotation = Mathf.Atan2(rotatedDirection.y, rotatedDirection.x) * Mathf.Rad2Deg - 90f;

            GameObject proj = Instantiate(defaultShotPrefab, transform.position, Quaternion.Euler(0f, 0f, zRotation));

            SpriteRenderer renderer = proj.GetComponent<SpriteRenderer>();
            Weapon weapon = proj.GetComponent<Weapon>();

            if (renderer != null)
            {
                renderer.flipX = rotatedDirection.x < 0;
            }

            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.velocity = rotatedDirection * weapon.speed;
            }
        }

    }
}

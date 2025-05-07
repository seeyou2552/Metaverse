using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Weapon weapon;
    private SurvivorPlayer player;
    private Enemy enemy;
    public float destroyTime = 5f;

    void Start()
    {
        if (this.transform.parent == null)
        {
            Destroy(this.gameObject, destroyTime);
        }
        else
        {
            Destroy(this.transform.parent.gameObject, destroyTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("몬스터 피격");
            OnDamage(other);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnDamage(Collider2D other)
    {
        GameObject obj = GameObject.FindWithTag("Player");
        player = obj.GetComponent<SurvivorPlayer>();
        weapon = GetComponent<Weapon>();
        enemy = other.GetComponent<Enemy>();
        enemy.hp -= (player.pow + weapon.pow);
    }


}

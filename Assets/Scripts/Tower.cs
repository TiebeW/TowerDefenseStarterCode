using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f; // Range within which the tower can detect and attack enemies 
    public float attackRate = 1f; // How often the tower attacks (attacks per second) 
    public int attackDamage = 1; // How much damage each attack does 
    public float attackSize = 1f; // How big the bullet looks 
    public float projectileSpeed = 10f; //Speed of projectile
    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
    public Enums.TowerType type; // the type of this tower 

    private float nextAttackTime; // Time when the tower last attacked

    // Draw the attack range in the editor for easier debugging 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            ScanForEnemiesAndShoot();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void ScanForEnemiesAndShoot()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                ShootAtEnemy(col.gameObject);
                break;
            }
        }
    }


    void ShootAtEnemy(GameObject enemy)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);

        Projectile projectile = bullet.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.damage = attackDamage;
            projectile.target = enemy.transform;
            projectile.speed = projectileSpeed;
        }
        else
        {
            Debug.LogError("Projectile component not found on bulletPrefab.");
        }
    }
}

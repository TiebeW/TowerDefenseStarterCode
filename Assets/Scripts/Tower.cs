using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f; // Range within which the tower can detect and attack enemies 
    public float attackRate = 1f; // How often the tower attacks (attacks per second) 
    public int attackDamage = 1; // How much damage each attack does 
    public float attackSize = 1f; // How big the bullet looks 
    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
    public TowerType type; // the type of this tower 

    private float lastAttackTime; // Time when the tower last attacked

    // Draw the attack range in the editor for easier debugging 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        if (Time.time - lastAttackTime > 1f / attackRate)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Find all enemies within attack range
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If no enemies found, return
        if (enemies.Length == 0)
            return;

        // Find the closest enemy within attack range
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        // Instantiate a bullet
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Set bullet properties
        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.damage = attackDamage;
            projectile.target = closestEnemy.transform;
        }

        // Set bullet scale
        bullet.transform.localScale = new Vector3(attackSize, attackSize, 1);
    }
}

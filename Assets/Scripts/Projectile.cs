using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    void Start()
    {
        // Draai het projectiel naar het doelwit
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Debug.LogWarning("No target assigned to projectile.");
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); //vernietig dit projectiel als het doelwit niet meer bestaat
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
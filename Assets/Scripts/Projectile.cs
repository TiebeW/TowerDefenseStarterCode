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
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }

    void Update()
    {
        // Controleer of het doelwit nog bestaat
        if (target == null)
        {
            Destroy(gameObject); // Vernietig dit object als het doelwit niet meer bestaat
            return;
        }

        // Beweeg het projectiel naar het doelwit
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Controleer of het projectiel het doelwit heeft bereikt
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= 0.2f)
        {
            // Voeg schade toe aan het doelwit
            DealDamage(target.gameObject);

            // Vernietig dit object
            Destroy(gameObject);
        }
    }

    void DealDamage(GameObject target)
    {
        // Voeg schade toe aan het doelwit
        // Implementeer je eigen logica om schade toe te brengen aan het doelwit
        // Bijvoorbeeld: target.GetComponent<Health>().TakeDamage(damage);
    }
}

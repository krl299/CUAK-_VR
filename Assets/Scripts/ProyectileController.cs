using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileController : MonoBehaviour
{

    public int damage;

    public int Damage { get => damage; set => damage = value; }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<ZombieController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().DamagePlayer(damage);
        else if (other.CompareTag("Map"))
            Destroy(gameObject);
    }

}

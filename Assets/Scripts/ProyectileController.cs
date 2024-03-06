using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileController : MonoBehaviour
{

    public int damage;

    public int Damage { get => damage; set => damage = value; }

    [SerializeField] private Target target;
    private enum Target 
    {
        Player,
        Enemy
    };

    private void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == Target.Enemy && other.CompareTag("Enemy"))
        {
            other.GetComponent<ZombieController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (target == Target.Player && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
}

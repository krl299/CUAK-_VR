using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lives")]
    public int currentLives;
    [SerializeField] private int maxLives;

    private Camera camera;
    private Rigidbody rb;
    //private GunController weaponController;

    public int MaxLives { get => maxLives; set => maxLives = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        //weaponController = GetComponent<GunController>();

    }

    private void Start()
    {
        //Update Player Healt Bar with the max 
        //HUDController.instance.UpdateHealthBar(MaxLives);
    }

    public void DamagePlayer(int quantity)
    {
        currentLives -= quantity;
        //HUDController.instance.UpdateHealthBar(currentLives);

        //HUDController.instance.ShowDamageFlash();

        if (currentLives <= 0)
        {

            //GameManager.Instance.LoseGame();

            Debug.Log("GAME OVER!!!");
        }

    }
}

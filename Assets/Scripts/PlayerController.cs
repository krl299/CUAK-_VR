using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lives")]
    public int currentLives;
    [SerializeField] private int maxLives;
    [SerializeField] private GameObject hudPlayer;
    private int score;

    private void Start()
    {
        hudPlayer.SetActive(false);
    }

    public void DamagePlayer(int quantity)
    {
        currentLives -= quantity;

        if (currentLives <= 0)
        {
            //TODO: Game Over Panel and Win Panel
            StartCoroutine(ReturnMainScene(5, false));
            Debug.Log("ZAMATAOOO!!!");
        }
    }

    public IEnumerator ReturnMainScene(int seconds, bool live)
    {
        hudPlayer.SetActive(true);
        if (live)
        {
            hudPlayer.GetComponentInChildren<TextMeshProUGUI>().text =
                "You Win!\nScore: " + score.ToString();
        }
        else
        {
            hudPlayer.GetComponentInChildren<TextMeshProUGUI>().text =
                "You Lose!\nScore: " + score.ToString();
        }
        yield return new WaitForSeconds(seconds);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Win"))
        {
            StartCoroutine(ReturnMainScene(5, true));
        }
    }

    public void AddScore(int quantity)
    {
        score += quantity;
    }
}

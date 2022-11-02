using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] public Player playerScript;
    AudioManager aM;
    private void Start()
    {
        aM = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Coin")
        {
            aM.PlaySound("CoinPickup");
            Destroy(other.gameObject);
            Manager.AddCoins();
        }
    }
}

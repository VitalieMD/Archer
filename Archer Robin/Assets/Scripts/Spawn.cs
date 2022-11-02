using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    private EnemyHealth [] enemyhealthScript;
    private Player playerScript;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyhealthScript = FindObjectsOfType<EnemyHealth>();
        playerScript = FindObjectOfType<Player>();
    }

    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + 2, player.position.y - 1);
        Instantiate(item, playerPos, Quaternion.identity);
    }

    public void DamagePotion()
    {
        foreach(EnemyHealth eH in enemyhealthScript)
        {
            eH.extraDamageIsActive = true;
            Destroy(gameObject);
        }
        
    }

    public void AddHP()
    {
        playerScript.AddHP(25);
        Destroy(gameObject);
    }
    

}

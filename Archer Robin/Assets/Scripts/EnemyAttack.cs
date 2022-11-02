using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float waitToAttack = 0.7f;
    [SerializeField] float waitToAttackReseter = 0.7f;
    bool isTouching;
   
    Player playerScript;

    private void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (isTouching)
        {
            waitToAttack -= Time.deltaTime;
            if(waitToAttack <= 0)
            {
                playerScript.AttackPlayer(damage);
                waitToAttack = waitToAttackReseter;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            playerScript.AttackPlayer(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            waitToAttack -= Time.deltaTime;
            if (waitToAttack <= 0)
            {
                playerScript.AttackPlayer(damage);
                waitToAttack = waitToAttackReseter;
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            waitToAttack = waitToAttackReseter;
        }

        
    }
}

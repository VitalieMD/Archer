using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;
    [SerializeField] public GameObject[] loot;
    [SerializeField] public int arrowDamage;
    [SerializeField] float extraDamageCountdown;
    [SerializeField] float extraDamageReseter;
    [SerializeField] public bool extraDamageIsActive;

    void Start()
    {
        extraDamageIsActive = false;
        currentHealth = maxhealth;
    }

    private void Update()
    {
        ExtraDamage();
    }

    public void ExtraDamage()
    {
        if (extraDamageIsActive)
        {
            arrowDamage = 20;
            extraDamageCountdown -= Time.deltaTime;
            if (extraDamageCountdown <= 0)
            {
                arrowDamage = 10;
                extraDamageCountdown = extraDamageReseter;
                extraDamageIsActive = false;
            }
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Arrow")
        {
            HurtEnemy(arrowDamage);
            print(arrowDamage);
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        currentHealth -= damageToGive;
        if(currentHealth <= 0)
        {
            Instantiate(loot[Random.Range(0, loot.Length)], gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
            Destroy(gameObject);
        }
    }
}

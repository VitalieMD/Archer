using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float arrowLifeSpan;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);

        Destroy(gameObject, arrowLifeSpan);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if (col.tag == "MapCollider")
        {
            Destroy(gameObject);
        }
    }

}

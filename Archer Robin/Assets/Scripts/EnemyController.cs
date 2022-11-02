using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector2 homePos;
    [SerializeField] Animator anim;
    Transform target;
    AudioManager audioManager;
    public EnemyHealth enemyhealthScript;
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float maxRange;
    [SerializeField] float minRange;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        homePos = transform.position;
        target = FindObjectOfType<Player>().transform;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if(Vector3.Distance(target.position,transform.position) <= maxRange && Vector3.Distance(target.position, transform.position)>= minRange)
        {
            FollowPlayer();
            audioManager.PlaySound("MonsterAttack");
        }
        else if (Vector3.Distance(target.position, transform.position)>=maxRange)
        { 
            ReturnHome();
            
        }  
        
    }

    public void FollowPlayer()
    {
        
        anim.SetBool("isMoving",true);
        anim.SetFloat("MoveX", (target.position.x - transform.position.x));
        anim.SetFloat("MoveY", (target.position.y - transform.position.y));
        rb.position = Vector3.MoveTowards(rb.position, target.transform.position, speed * Time.deltaTime);
    }

    public void ReturnHome()
    {
        anim.SetFloat("MoveX", (homePos.x - transform.position.x));
        anim.SetFloat("MoveY", (homePos.y - transform.position.y));
        rb.position = Vector3.MoveTowards(rb.position, homePos, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, homePos) == 0)
        {
            anim.SetBool("isMoving", false);
        }
    }
}

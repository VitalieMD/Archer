using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRB;
    [SerializeField] Animator animator;
    HealthBar healthBar;
    [SerializeField] GameObject arrowPrefab;        //Arrowprefab to Attack enemys 04.02.22
    [SerializeField] Transform firePoint;           //Point where Arrow instantiate 04.02.22
    AudioManager audioManager;



    [SerializeField] float moveSpeed = 5f;          //Player Move Speed
    [SerializeField] public int maxHealth;          //Player Max Health
    public int currentHealth;                       //Player Current Health
    [SerializeField] float waitToAttack = 0.5f;            //Time until next attack
    [SerializeField] float refreshTimeToAttack = 0.5f;
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking;

    Vector2 movement;
    
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        healthBar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        AnimatorController();
        ArrowInstantiation();
        FootStepsSound();
        myRB.MovePosition(myRB.position.normalized * moveSpeed * Time.deltaTime * movement);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Manager.AddCoins();
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        Attack();

        
    }

    public void AttackPlayer(int damagePower)
    {
        
         currentHealth -= damagePower;
         audioManager.PlaySound("TakeHit");
         healthBar.SetHealth(currentHealth);
         if (currentHealth <= 0)
         {
             audioManager.PlaySound("PlayerDie");
             gameObject.transform.position = new Vector3(45.7f, -55.9f, 0);
             currentHealth = maxHealth;
             healthBar.SetHealth(currentHealth);
         }

    }

    void Attack() //Arrow Direction when instantiate
    {
        if (movement.x <= -0.1f)
        {
            firePoint.transform.localPosition = new Vector3(-0.6f, 0f, 0f);
            firePoint.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (movement.x >= 0.1f)
        {
            firePoint.transform.localPosition = new Vector3(0.6f, 0f, 0f);
            firePoint.transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        else if (movement.y <= -0.1f)
        {
            firePoint.transform.localPosition = new Vector3(0f, -0.6f, 0f);
            firePoint.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (movement.y >= 0.1f)
        {
            firePoint.transform.localPosition = new Vector3(0f, 0.6f, 0f);
            firePoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void PlayerMovement() //Player Movement
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void ArrowInstantiation()
    {
        waitToAttack -= Time.deltaTime;
        if(waitToAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                audioManager.PlaySound("Bow");
                Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
                attackCounter = attackTime;
                animator.SetBool("isAttacking", true);
                isAttacking = true;
                waitToAttack = refreshTimeToAttack;
            } 
        }
    }
    void AnimatorController()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("lastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastVertical", Input.GetAxisRaw("Vertical"));
        }

        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }
    }
    void FootStepsSound() //Activate footStep sound effect
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            
        }
        else
        {
            audioManager.PlaySound("FootSteps");
        }
    }

    public void AddHP(int healthPoints) // Add HP with health potions
    {
        if(currentHealth + healthPoints <= maxHealth)
        {
            currentHealth += healthPoints;
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth + healthPoints > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    } 

}

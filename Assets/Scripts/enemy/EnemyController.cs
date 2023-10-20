using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    [SerializeField] private int movementDistance;
    [SerializeField] private int speed;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int damageAttack;
    [SerializeField] private float attackCooldown = 1.5f;
    private float nextAttack;
   

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge; 
    private Animator anim;
    private int currentHealth;

    private bool playerDetected;


    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
// Sets how far are the left and right edge 
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        nextAttack = attackCooldown;

        
    }

    private void Update(){

// Simple timer for enemy to attackCooldown, in awake next attack is assigned a valaue for example three
// If the value is bigger than 0 than takeAway time timedelta (time since last frame) which correspondents to 3 secs 
// then if nextattack is less or equal to zero enemy can attack player, if we didnt have this timer
// enemy would just spam the player with attacks and immedietly kill him 
// When attacked the cooldown is reset 
// This simple code I found on unity forum from person named tormentoarmagedoom : https://discussions.unity.com/t/i-need-help-making-my-enemy-ai-have-an-attack-delay/208056/2
        if(nextAttack > 0){
            nextAttack -=Time.deltaTime;
        } else if (nextAttack <= 0){
            AttackPlayer();
            nextAttack = attackCooldown;
        }

        EnemyMovement();
        
        // var collider = Physics2D.OverlapCircle(transform.position, attackRange, playerMask);
        // playerDetected = collider != null;
        // if(playerDetected){
        //     collider.GetComponent<Health>().GetDamaged(damageAttack);
        //     anim.SetTrigger("attack");
        // }



    }

// Function that is called inside playerAtack takes in int for how much player hurt the enemy 
// this amount is taken away from his current health and hurt animation is set 
    public void TakeDamage(int damageHurt){
        currentHealth -= damageHurt;
        anim.SetTrigger("hurt");

// If enemy helath gets to zero, we call die function to kill/remove him 
        if(currentHealth <= 0){
            Die();
        }

    }

// When enemy dies set animation to dead and disable the gameObject 
    private void Die(){
        anim.SetTrigger("dead");
        gameObject.SetActive(false);
    }

// For moving the player I used the same logic as in the sideway saw trap plus adding flip and animations
// This could be done so much better, for example follow player when he enters a raycast box, than just walking from side to side
// but due to being in a rush I decided to leave this simple and come back to it if I finish with spare time 
    private void EnemyMovement(){
          if(movingLeft){
            if(transform.position.x > leftEdge){
                transform.position = new Vector3(transform.position.x - speed* Time.deltaTime, transform.position.y, transform.position.z);
                anim.SetBool("isRunning", true);
            }else {
                movingLeft = false;
                Flip();

            }
        } else {
            if(transform.position.x < rightEdge){
                transform.position = new Vector3(transform.position.x + speed* Time.deltaTime, transform.position.y, transform.position.z);

                anim.SetBool("isRunning", true);
            } else {
                movingLeft = true;
                Flip();
            }
        }
    }

    
    // Same logic as in player attack but without making an array since player is just one 
    // we create a variable assigned with imaginery circle that filters only objects with playerMask
    // then create a variable player detected and assign it variable above doesnt equal null
    // If playerdetected is true then call on health script and damage the player with getDamaged plus play enemy attack animation 
    // This code was taken from "Sunny Valey Studio" on youtube: https://www.youtube.com/watch?v=uOobLo2y3KI&t=635s
    private void AttackPlayer(){
        var collider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask);
        playerDetected = collider != null;
        if(playerDetected){
            collider.GetComponent<Health>().GetDamaged(damageAttack);
            anim.SetTrigger("attack");
        }
    }

// Reversing enemy's x axes 
    private void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }


    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

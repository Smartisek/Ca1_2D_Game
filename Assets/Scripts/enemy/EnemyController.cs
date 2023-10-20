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

        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        nextAttack = attackCooldown;

        
    }

    private void Update(){

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

    public void TakeDamage(int damageHurt){
        currentHealth -= damageHurt;
        anim.SetTrigger("hurt");

        if(currentHealth <= 0){
            Die();
        }

    }

    private void Die(){
        anim.SetTrigger("dead");
        gameObject.SetActive(false);
    }

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

    
    private void AttackPlayer(){
        var collider = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerMask);
        playerDetected = collider != null;
        if(playerDetected){
            collider.GetComponent<Health>().GetDamaged(damageAttack);
            anim.SetTrigger("attack");
        }
    }

    private void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    // private void AttackPlayer(){
       
    //     var collider = Physics2D.OverlapCircle(transform.position, attackRange, playerMask);
    //     playerDetected = collider != null;
    //     if(playerDetected){
    //         GetComponent<Health>().GetDamaged(damageAttack);
    //         anim.SetTrigger("attack");
    //     }

       
    // }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

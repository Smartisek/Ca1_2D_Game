using System.Collections;
using System.Collections.Generic;
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

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge; 
    private Animator anim;
    private int currentHealth;
    private bool canChangeDirection;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        
    }

    private void Update(){

        print(currentHealth);

        EnemyMovement();

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

private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().GetDamaged(damageAttack);
        }
    }
    // private void AttackPlayer(){

    //     anim.SetTrigger("attack");
        
    //     Collider2D [] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerMask);

    //     foreach(Collider2D player in hitPlayer){
    //         player.GetComponent<Health>().GetDamaged(damageAttack);
    //     }
    // }

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

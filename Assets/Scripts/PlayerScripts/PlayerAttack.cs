using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] knifesHolder;

    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int dealDamage;

    private Animator anim;
    private PlayerMovement playerMovement;
// For start set to infinity otherwise could never use attack because of the condition in update 
    private float cooldownTimer = Mathf.Infinity;
  
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
// Using left mouse button for throwing knife if these conditions are met 
        if(Input.GetMouseButtonDown(1) && cooldownTimer > attackCooldown && playerMovement.CanAttack()){
            AttackKnife();
            cooldownTimer += Time.deltaTime; 
        }

        if(Input.GetMouseButtonDown(0) && playerMovement.CanAttack()){
            AttackMelee();
        }

    }

// function for throwing knifes, set animation and reset cooldown, then makes the knife fly in the right direction
// knifesHolder is an object in unity that has all the prefab knifes inside so it is an array, we set their transform position 
// to be a firePoint position which is and empty object under player that just sets knifes "spawner"
    private void AttackKnife(){
        anim.SetTrigger("throwKnife");
        cooldownTimer =0;
    // pooling method
    // inside knife holder for each (FindKnife) knife get its script component and use its SetDirection passing in current MathF Sign of players position 
    // meaning either 1 or -1 depending on which way he is facing, if left it will get -1 and send it that way and the otherway around 
    knifesHolder[FindKnife()].transform.position = firePoint.position;
    knifesHolder[FindKnife()].GetComponent<ThrowingKnifeScript>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

// We need to loop thorugh the array of knife so for loop has to be used to loop through the objects inside of container 
    private int FindKnife(){
        // The if statement means if knife of i index is not active then return i, otherwise if its active return 0, this way the program knows 
        // which ones are active and inactive
        for(int i =0; i < knifesHolder.Length; i++){
            if(!knifesHolder[i].activeInHierarchy){
                return i;
            }
        }
        
        return 0;
    }

// Creates a collider2d array of all the enemies player hits
// Inside unity I create an object attackPoint and then with physics2d overlapcircle all function draw a circle around this object 
// this functiom takes in the positon, range and layer, my layer is set to enemies
    private void AttackMelee(){
        anim.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

// We loop through our array of enemies hit and call on function inside enemy controller takedamage to damage the enemy with chosen amount 
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<EnemyController>().TakeDamage(dealDamage);
        }
    }

// Draw Gizmos draws me an imaginery circle that i created in the function above, without it i wouldnt be able to see the circle 
// if object attackpoint is null than just return and dont do anything, otherwise draw a speher with position of an object and attackrange 
    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
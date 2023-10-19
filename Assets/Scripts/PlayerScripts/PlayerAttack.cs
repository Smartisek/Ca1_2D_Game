using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] knifesHolder;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
  
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
// Using left mouse button for throwing knife if these conditions are met 
        if(Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack()){
            AttackKnife();
            cooldownTimer += Time.deltaTime; 
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
}
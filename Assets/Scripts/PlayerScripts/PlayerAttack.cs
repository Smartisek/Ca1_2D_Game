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
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack()){
            AttackKnife();
            cooldownTimer += Time.deltaTime; 
        }

    }

    private void AttackKnife(){
        anim.SetTrigger("throwKnife");
        cooldownTimer =0;
    // pooling
    knifesHolder[FindKnife()].transform.position = firePoint.position;
    knifesHolder[FindKnife()].GetComponent<ThrowingKnifeScript>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindKnife(){
        
        for(int i =0; i < knifesHolder.Length; i++){
            if(!knifesHolder[i].activeInHierarchy){
                return i;
            }
        }
        
        return 0;
    }
}
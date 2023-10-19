using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float fullhealth;
    private Animator anim;
    public float currentHealth {get ; private set;}
    private bool isDead;


    private void Awake(){
        currentHealth = fullhealth;
        anim = GetComponent<Animator>();

    }

    public void GetDamaged(float damage){
        currentHealth =  Mathf.Clamp(currentHealth - damage, 0, fullhealth);

        if(currentHealth > 0 ){
            // Get hurt
            anim.SetTrigger("hurt");
        } else {
            if(!isDead){
            anim.SetTrigger("die");
             GetComponent<PlayerMovement>().enabled = false; 
            }
            
        }
    } 

    private void Update(){
        // Test if healthbar works
        //  if(Input.GetKeyDown(KeyCode.E)){
        //     getDamaged(1);
        // }
    }

    public void AddHealth(float value){
        currentHealth =  Mathf.Clamp(currentHealth + value, 0, fullhealth);
    }


}

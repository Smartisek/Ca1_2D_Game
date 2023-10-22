using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
// For this whole health system I worked with tutorial from Pandemonium on Youtube: https://www.youtube.com/watch?v=yxzg8jswZ8A&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=7

    [SerializeField] private float fullhealth;
    private Animator anim;
    public float currentHealth {get ; private set;}
    private bool isDead;
    Vector2 startPosition;
    

// When start set the current health to be full and acces animator 
    private void Awake(){
        currentHealth = fullhealth;
        anim = GetComponent<Animator>();
        startPosition = transform.position;
    }

// function for taking damage, takes in float for how much hurt player will get 
// Mathf.Clamp takes in three arguments, current min and max, for current we give it currentHealth minus damage we took, minimum can only be zero 
// and full health which is set inside unity through serialize field 
    public void GetDamaged(float damage){
        currentHealth =  Mathf.Clamp(currentHealth - damage, 0, fullhealth);
        AudioManager.instance.PlayHurtSound();
// If players current health is more than zero meaning he isnt dead then we play hurt animation 
        if(currentHealth > 0 ){
            // Get hurt
            anim.SetTrigger("hurt");
// Else if players currenthealth is less than zero and is not dead yet we enable his playerMovement and play dead animation 
        } else {
            if(!isDead){
            anim.SetTrigger("die");
            AudioManager.instance.PlayDieSound();
            StartCoroutine(Respawn(1f));
             GetComponent<PlayerMovement>().enabled = false;
             GetComponent<PlayerAttack>().enabled = false;
             StartCoroutine(RespawnMovementDisabled(1.5f));
            }
            
        }
    } 

    private void Update(){
        // Test if healthbar works
        //  if(Input.GetKeyDown(KeyCode.E)){
        //     getDamaged(1);
        // }
    }

// Function for HealthRecharge when picking up a heart from a ground, same like get damage function but we give plus to current health the value of picked heart 
    public void AddHealth(float value){
        currentHealth =  Mathf.Clamp(currentHealth + value, 0, fullhealth);
    }

// Respawn code from Rehope Games on Youtube: https://www.youtube.com/watch?v=odStG_LfPMQ
// When coroutine called its gonna wait duration seconds and then execute code below 
// respawn to start position and set health to full
    IEnumerator Respawn(float duration){
        yield return new WaitForSeconds(duration);
        transform.position = startPosition;
        currentHealth = fullhealth;
    }

// Gives player back his movement and attack ability
    IEnumerator RespawnMovementDisabled(float value){
        yield return new WaitForSeconds(value);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float fullhealth;
    private Animator anim;
    public float currentHealth {get ; private set;}


    private void Awake(){
        currentHealth = fullhealth;
        anim = GetComponent<Animator>();

    }

    private void getDamaged(float damage){
        currentHealth =  Mathf.Clamp(currentHealth - damage, 0, fullhealth);

        if(currentHealth > 0 ){
            // Get hurt
            anim.SetTrigger("hurt");
        } else {
            anim.SetTrigger("die");
        }
    } 

    private void Update(){
        // Test if healthbar works
        //  if(Input.GetKeyDown(KeyCode.E)){
        //     getDamaged(1);
        // }
    }


}

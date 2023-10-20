using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private Animator anim;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        
    }

    private void Update(){

        print(currentHealth);
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        anim.SetTrigger("hurt");

    }


  
}

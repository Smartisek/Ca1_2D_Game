using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesCollision : MonoBehaviour
{
   
    [SerializeField] private int damage;


// Simple trigger collision alredy used in other scripts, if object with tag  player enters collider then deal him damage 
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().GetDamaged(damage);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthRecharge : MonoBehaviour
{
   [SerializeField] private float healthValue;

// When player collides with this object then we call on script Health and use its AddHealth function with healtValue set inside unity
// If collision tag == "Player", I have a tag on players object so it knows when it collides with player 
// Then when collide and recharged just disable this object 
  private void OnTriggerEnter2D(Collider2D collision){
    if(collision.tag == "Player"){
        
        collision.GetComponent<Health>().AddHealth(healthValue);
        gameObject.SetActive(false);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthRecharge : MonoBehaviour
{
   [SerializeField] private float healthValue;

  private void OnTriggerEnter2D(Collider2D collision){
    if(collision.tag == "Player"){
        
        collision.GetComponent<Health>().AddHealth(healthValue);
        gameObject.SetActive(false);
    }
  }
}

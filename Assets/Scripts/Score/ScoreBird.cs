using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBird : MonoBehaviour
{

    // Using same logic I learned in healthRecharge script, when player collides with this object then is calls on a function increase score in score counter script 
    // and destroys the object 
   private void OnTriggerEnter2D(Collider2D collision){
    if(collision.tag == "Player"){
        collision.GetComponent<ScoreCounter>().IncreaseScore();
        AudioManager.instance.PlayScoreSound();
        gameObject.SetActive(false);
    }
  }
}

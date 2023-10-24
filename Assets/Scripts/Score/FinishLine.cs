using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
  public GameManager gameManager;

// When player enters collider than call on finish game in game manager 
  private void OnTriggerEnter2D(Collider2D collision){
    if(collision.tag == "Player"){
        gameManager.FinishGame();
    }
  }

}

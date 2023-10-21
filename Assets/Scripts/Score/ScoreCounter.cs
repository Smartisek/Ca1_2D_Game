using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{   
    // reference to a text that will increase when we get a bird 
    [SerializeField] TMP_Text scoreText;
    private int currentScore = 0;

    // assign 0 score when start 
    private void Start(){
        scoreText.text = currentScore.ToString();
    }

// Increase by one when called and increased score display in the text with ToString method 
// This function is called in other script when collided (in ScoreBird that is component of a player)
// Similar function as in options settings 
    public void IncreaseScore(){
        currentScore +=1;
        scoreText.text = currentScore.ToString();
    }




}

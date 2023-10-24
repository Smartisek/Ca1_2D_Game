using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScoreCounter : MonoBehaviour
{   
    // reference to a text that will increase when we get a bird 
    [SerializeField] TMP_Text scoreText;
     int currentScore = 0;
    [SerializeField] TMP_Text finalScore;

    // assign 0 score when start and display it as a string 
    private void Start(){
        scoreText.text = currentScore.ToString();
       
    }



// Increase by one when called and increased score display in the text with ToString method 
// This function is called in other script when collided (in ScoreBird that is component of a player)
// Similar function as in options settings 
    public void IncreaseScore(){
        currentScore +=1;
        scoreText.text = currentScore.ToString();
        finalScore.text = currentScore.ToString();
    }

}

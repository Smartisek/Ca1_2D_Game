using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

[Header("Game Manager")]
    public bool isPaused;
    public bool isGameOver;

// Singleton, is an object that gets created only once in a game cycle, if there is any duplicate destroy it 
// From Naoise's code in class, I did this whole script based on our last class 
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            LoadMainMenu();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            if(isPaused){
                ResumeGame();
            } else{
                PauseGame();
            }
        }
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame(){
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        isPaused = false;
        Time.timeScale = 1f;
    }
}

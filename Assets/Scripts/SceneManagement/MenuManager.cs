using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private String gameSceneName;
    [SerializeField] private String optionSceneName;
    
    public void StartGame(){
        SceneManager.LoadScene(gameSceneName);
    }

    public void OptionsMenu(){
        SceneManager.LoadScene(optionSceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }

}

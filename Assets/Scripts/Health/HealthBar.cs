
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

// Variables for accessing images in unity for UI hearts, one with normal and other with black ones  plus access players health in health script
  [SerializeField] private Health playerHealth;
  [SerializeField] private Image FullHealth;
  [SerializeField] private Image CurrentHealth;


// At start project full health set of hearts, settings it fill amount that controls taking away hearts 
// playerHealth.current health is from script Health 
  private void Start(){
        FullHealth.fillAmount = playerHealth.currentHealth / 10;
  }

  private void Update(){
        CurrentHealth.fillAmount = playerHealth.currentHealth / 10;
  }

}

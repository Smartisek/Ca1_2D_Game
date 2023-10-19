
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  
  [SerializeField] private Health playerHealth;
  [SerializeField] private Image FullHealth;
  [SerializeField] private Image CurrentHealth;


  private void Start(){
        FullHealth.fillAmount = playerHealth.currentHealth / 10;
  }

  private void Update(){
        CurrentHealth.fillAmount = playerHealth.currentHealth / 10;
  }

}

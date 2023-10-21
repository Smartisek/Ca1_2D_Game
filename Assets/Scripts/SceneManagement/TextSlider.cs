using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI numberText;
   private Slider slider;

   public void Start(){
    slider = GetComponent<Slider>();
    SetVolumeNumber(slider.value);
   }

   public void SetVolumeNumber(float value){
    numberText.text = value.ToString();
   }
}

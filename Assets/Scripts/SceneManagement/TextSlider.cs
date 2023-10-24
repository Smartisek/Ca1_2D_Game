using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
// Followed tutorial from Chris' Tutorials on youtube: https://www.youtube.com/watch?v=FfaG9TvCe5g&t=14s
   [SerializeField] TextMeshProUGUI numberText;
   private Slider slider;

   public void Start(){
    slider = GetComponent<Slider>();
    SetVolumeNumber(slider.value);
    AudioListener.volume = slider.value/2;
   }

   public void SetVolumeNumber(float value){
      AudioListener.volume = value/2;
    numberText.text = value.ToString();
   }
}

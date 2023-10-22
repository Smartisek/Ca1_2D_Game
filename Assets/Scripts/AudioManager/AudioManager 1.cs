using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  
    public static AudioManager instance;

    [Header("Game Sounds")]
        public AudioClip jump;
        public AudioClip swordAttack;
        public AudioClip gettingHurt;
        public AudioClip score;
        public AudioClip knifeThrow;
        public AudioClip backgroundMusic;
        public AudioClip die;
        public AudioClip health;

        private AudioSource soundEffectSource;
        private AudioSource backgroundMusicSource;

// Singleton, is an object that gets created only once in a game cycle, if there is any duplicate destroy it 
// From Naoise's code in class, I did this whole script based on our last class 
        public void Awake(){
            if(instance == null){
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
                return;
            }

        soundEffectSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

// Loop the background music so it doesnt stop playing when whole song finishes 
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
        }

        public void PlayJumpSound(){
            soundEffectSource.PlayOneShot(jump);
        }

        public void PlayScoreSound(){
            soundEffectSource.PlayOneShot(score);
        }

        public void PlaySwordAttack(){
            soundEffectSource.PlayOneShot(swordAttack);
        }

        public void PlayKnifeThrow(){
            soundEffectSource.PlayOneShot(knifeThrow);
        }

        public void PlayHurtSound(){
            soundEffectSource.PlayOneShot(gettingHurt);
        }

        public void PlayDieSound(){
            soundEffectSource.PlayOneShot(die);
        }

        public void PlayPickHealth(){
            soundEffectSource.PlayOneShot(health);
        }



}

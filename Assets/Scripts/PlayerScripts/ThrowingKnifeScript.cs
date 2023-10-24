using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeScript : MonoBehaviour
{

    // Script followed by Pandemonium on Youtube: https://www.youtube.com/watch?v=PUpC44Q64zY&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=4
[Header("Knife Variables")]
    [SerializeField] private float speed;
    [SerializeField] private int knifeDamage;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator anim;
    private void Awake(){

        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
// If hit is true it will return and not execute the rest of the code bellow
        if(hit) return;
// We need a variable of which speed our knife will move, we assign this variable a speed variable (which is serialized number, will be assigned inside unity)
// multiplied by time deltatime (time since last frame) and direction (that is assigned in playerattack with _direction, which thanks to mathF sign will be either 
//  1 or -1 so this will depend if knife will go right or left) 
// Then we just move it by translate on x axes with our movementspeed from above 
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

// Since I am using pulling method I need to get the knifes back in my box if they do not hit anything 
// for that there is lifetime variable that goes up by time delte, sice time delta is in seconds this statement checks if lifetime is 
// bigger than 2 (seconds) and then just deactivates the object, giving it back to me in the box 
        lifeTime += Time.deltaTime;
        if(lifeTime > 2){
            gameObject.SetActive(false);
        }
    }

// When object collides set the hit boolean true and enable its collider, set animation to explode (My knife doesnt really explode but I tried to 
//  get in animation for it)

private void OnTriggerEnter2D(Collider2D collision){
// this function looks like it is missing deactivate function when collision triggered but that is because I used on event for the last frame of knife "explode"
// animation and that last "frame" of it has an event which calls on deactivate function, it could be done from this code as well just by 
// calling Deactivate();
    hit = true;
    boxCollider.enabled = false;
    anim.SetTrigger("explode");

    if(collision.tag == "EnemyTag"){
        collision.GetComponent<EnemyController>().TakeDamage(knifeDamage);
    }
}

// When the player actually throws a knife, we need to know which way he is facing and send knife the same direction 
// This function is called in PlayerAttacks and when called it makes the object active, and its collider (I use pooling method, so I have a few duplicates of a prefab so when i need them 
//  I only have to activate them), the function takes in a float _direction (local) passed in when called that is assigned to global direction
// lifetime is set to zero and hit to false because it was just spawned 
// To know where this knife is we assign localScaleX a transform localScale x which assigns it with current x axes of the object 
public void SetDirection(float _direction){
    
    gameObject.SetActive(true);
    boxCollider.enabled = true;

    direction = _direction;
    hit = false;
    lifeTime =0;
    float localScaleX = transform.localScale.x;

// Mathf Sign will return 1 or -1 and if it does not equal direction (another Mathf in playerattack) first passed in the localScaleX of the knife will flip with - 
         if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;
// A new direction will be assigned with vector with this localScaleX, either the one we originally passed in in player attack or flip if we are facing other way 
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
}

// Function for deactivation knife once collides or when its lifetime is up 
private void Deactivate(){
    gameObject.SetActive(false);
}

}

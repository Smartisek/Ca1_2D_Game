using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

     private void Awake(){
// Sets how far the edges are 
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    // Simple movement, if we are movingLeft and its position is more than the left edge than move to the other side, else if its not at the edge movingLeft is false
// Else if moving left is not true and its position is more than right edge  
    private void Update(){
        if(movingLeft){
            if(transform.position.x > leftEdge){
                transform.position = new Vector3(transform.position.x - speed* Time.deltaTime, transform.position.y, transform.position.z);
            }else {
                movingLeft = false;
            }
        } else {
            if(transform.position.x < rightEdge){
                transform.position = new Vector3(transform.position.x + speed* Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = true;
            }
        }
    }


// If this collider collides with another collider and that collider has object name Player then set that game object to be child of this transform (platform)
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Player"){
            collision.gameObject.transform.SetParent(transform);
        }
    }
// When this collision ends we remove parent on that gameObject player with null 
// I got this solution from Coding in Flow on Youtube: https://www.youtube.com/watch?v=UlEE6wjWuCY&list=PLrnPJCHvNZuCVTz6lvhR81nnaf1a-b67U&index=9
   private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.name == "Player"){
        collision.gameObject.transform.SetParent(null);
    }

   }
}


using UnityEngine;

public class Enemy_SidewayTrap : MonoBehaviour
{
    [SerializeField] private float damage;
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

// If object collides with tag of Player than acces Health script and use GetDamage to hurt the player 
// Damage dealt is set in unity 
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().GetDamaged(damage);
        }
    }


  
}

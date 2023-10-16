using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class playerMovement : MonoBehaviour
{

// Variables for jumping power and movement speed accessable from inside unity 
    [SerializeField] public float jumpHeight = 5f;
    [SerializeField] public float moveSpeed = 5f;

// Referencing access to unity Components
    private Rigidbody2D bodyRigid;
    private BoxCollider2D boxCollider;
    private Animator animate;
// Accessing the layers in unity
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
// Variables to use in methods like facesRight for Flip()
     private float wallJumpCooldown;
     private float horizontalInput;
     private bool facesRight = true;

// Variables for sounds and accesing audio
private AudioSource audioPlayer;
public AudioClip jumpSound;
public AudioClip runningSound;

    // Start is called before the first frame update
    void Awake()
    {
     bodyRigid = GetComponent<Rigidbody2D>();
     animate = GetComponent<Animator>();
     audioPlayer = GetComponent<AudioSource>();
     boxCollider = GetComponent<BoxCollider2D>();



    }

    // Update is called once per frame
    void Update()
    {
// Assigning players horizontal input to horizontalInput variable and when it's axis are not 0, meaning he is moving
// then animation goes from idle to runnning 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        animate.SetBool("isRunning", horizontalInput !=0);

// Statement for flipping sides of player when running 
// when horizontalInput (meaning players horizontal axes) and boolean for facing right
// First condition means "x" is more than 0 so we are facing right but our facing right is false (we do not face right) needs to flip
// Second condition means "x" is less than 0 so we face,go left but our character is facing right is true so we need to flip him
// Statement calls flip function explained lower 
        if(horizontalInput>0 && !facesRight || horizontalInput <0 && facesRight){
            Flip();
        }

// When horizontalInput is not 0 meaning we are moving and we are on a ground (IsGrond true)
// Play running sound
        if(horizontalInput !=0 && IsGround()){
            PlaySound(runningSound);
        }
        
        if(wallJumpCooldown > 0.2f){
             bodyRigid.velocity = new Vector2(horizontalInput*moveSpeed, bodyRigid.velocity.y);
            if(IsOnWall() && !IsGround()){
            bodyRigid.gravityScale = 0;
            bodyRigid.velocity = Vector2.zero;
        } else {
            bodyRigid.gravityScale = 2;
        }
          if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
          }
        } else{
            wallJumpCooldown += Time.deltaTime;
        }
    }

// Function for jumping, when IsGround() is true we are able to move up by jumpHeight and animation changes to jump plus sound plays
    void Jump(){
        if(IsGround()){
            bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
            animate.SetTrigger("Jump");
             PlaySound(jumpSound);
        }else if(IsOnWall() && !IsGround()){
// Otherwise if IsOnWall is true and IsGround is false, meaning we are on a wall, and we are not "moving" (not going against the wall) then when
// pressing jump we create a new vector that pushes us away from the wall and calls Flip() that switches sides of player
            if(horizontalInput == 0){
               bodyRigid.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
               Flip();
// Else if IsOnWall is still true and we are not grounded is still false, but we are "pushing" against the wall (trying to climb it)
// we create a new vector that will push us other way with power of 3 and up with power of 6 which creates movement like this ) going up
            } else{
                PlaySound(jumpSound);
                 bodyRigid.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
            }
            wallJumpCooldown =0;
       
        }
        

    }

// Function for flipping character when facing left
// First gets current position and multiplies with -1 to flip it, then asigns this value back to currentScale and changes boolean to be false
    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facesRight = !facesRight;

    }

// Function for being able to play sounds 
    private void PlaySound(AudioClip clip){
            audioPlayer.clip = clip;
            audioPlayer.Play();
    }

// Method to check if we are on ground 
// Creates a box on player, vector facing down, angle (0) and checks if this box intersects with a layer, in this case with groundLayer
// Return statement returns null because when in the air there is nothing under the player so IsGround() will return false
    private bool IsGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    } 

// Method similar to IsGround but instead of vector facing down we need to check a side if we are on wall so we get players position that he is facing and
// then check if this box intersects with wallLayer
    private bool IsOnWall(){    
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    } 

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// These components will be required for this game object 
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

// For this script I mainly followed our script we worked on at school and then mainly for walljump i followed
// tutorial from Pandemonium on youtuobe : https://www.youtube.com/watch?v=_UBpkdKlJzE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=3 and
// https://www.youtube.com/watch?v=oFO4pgrQPOI&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=13

public class PlayerMovement : MonoBehaviour
{

 [Header("Jump Variables")]
    [SerializeField] public float jumpHeight = 5f;
    [SerializeField] private int maxJumps;
    private int jumpCounter;
    [SerializeField] private float wallJumpX; // Wall jumping variables 
    [SerializeField] private float wallJumpY; // Wall jumping variables 

[Header("Movement Variables")]
    [SerializeField] public float moveSpeed = 5f;
    private float horizontalInput;
    private bool facesRight = true; // Variable to use in methods like Flip()

// Referencing access to unity Components
    private Rigidbody2D bodyRigid;
    private BoxCollider2D boxCollider;
    private Animator animate;
    [SerializeField] private LayerMask groundLayer; // Accessing the layers in unity
    [SerializeField] private LayerMask wallLayer; // Accessing the layers in unity

   
  
    

// Variables for sounds and accesing audio
private AudioSource audioPlayer;
// public AudioClip jumpSound;

    // Start is called before the first frame update
    private void Awake()
    {
     bodyRigid = GetComponent<Rigidbody2D>();
     animate = GetComponent<Animator>();
     audioPlayer = GetComponent<AudioSource>();
     boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        JumpHandling();
        
    }

    private void Jump(){
// if player is on wall and has no extra jumps then dont do anything and return, code uder wont be executed
    if(!IsOnWall() && jumpCounter <=0) return;

// if player is on wall then allow wallJump
      if(IsOnWall()){
        WallJump();
      } else{
// if player is on ground then do jump, meaning accessing velocity with rigid body and create new vector that will move player up by jumpHeight
// then change animation and play sound  
        if(IsGround()){
            bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
            AudioManager.instance.PlayJumpSound();
            animate.SetTrigger("Jump");
// if jumpCounter is more than zero, then do another jump and decrease jumpCounter by one  
        } else{
            if(jumpCounter >0){
            bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
            AudioManager.instance.PlayJumpSound();
            animate.SetTrigger("Jump");
            jumpCounter--;
            }
        }
      }
    }


// using function addForce which takes in two parameters, force for X axes and force for Y axes
// X axes is current x position and multiplied by the force wallJumpX, Y axes gets force wallJumpY
// The effect that this AddForce with Mathf.Sign create is like being pushed in this trajectory ")"
// The MathF.Sign is a function that returns value between -1 and 1 (-1 when number is negative, and 1 when number is positive or zero), in this case it gets value of transform.localScale.x
// but we need the player to have an effect of being pushed away from the wall so we change the Sign value with - in front of Mathf.Sign
// then flip the player when beign pushed 
    private void WallJump(){
        bodyRigid.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)* wallJumpX, wallJumpY));
        animate.SetTrigger("Jump");
        AudioManager.instance.PlayJumpSound();
        Flip();
    }

// Function for flipping character when facing left
// First gets current position and multiplies with -1 to flip it, then asigns this value back to currentScale and changes boolean to be false
    private void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facesRight = !facesRight;

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

// If any of those conditions are not met then the function will return false, meaning player is not able to perform attack;
    public bool CanAttack(){
        return horizontalInput ==0 && !IsOnWall();
    }

    public void Run(){
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
    
        }
    }

private void JumpHandling(){
// If we are moving in plus and is not facing right flip, if moving in minus and facing right then flip
      if(horizontalInput>0 && !facesRight || horizontalInput <0 && facesRight){
            Flip();
        }

// When horizontalInput is not 0 meaning we are moving and we are on a ground (IsGrond true)
// Play running sound
    if(horizontalInput !=0 && IsGround()){
    
        }
        
    if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

// When the key is released and player is in the air then we make him fall faster by dividing its velocity.y by 2
// So we get to make "charge" jump with holding key longer in the statement above 
        if(Input.GetKeyUp(KeyCode.Space) && bodyRigid.velocity.y >0){
            bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, bodyRigid.velocity.y /2);
        }

// When player is on wall and is not trying to move any direection, then set gravity to 0 and vector to zero as well
// so there is no force pulling player down, we stick to the wall like spider-man, almost 
        if(IsOnWall()){
            bodyRigid.gravityScale = 0;
            bodyRigid.velocity = Vector2.zero;
// else if player tries to move back then we set gravity back to default and give him control to move again 
        } else {
            bodyRigid.gravityScale = 3;
            bodyRigid.velocity = new Vector2(horizontalInput * moveSpeed, bodyRigid.velocity.y);

// When player falls back to the ground then we reset the jumpCounter so he is able to use his extra jump/jumps
            if(IsGround()){
                jumpCounter = maxJumps;
            }
        }

        // print(MathF.Sin(transform.localScale.x));
    }

}

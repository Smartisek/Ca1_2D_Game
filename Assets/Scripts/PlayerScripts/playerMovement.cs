using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class playerMovement : MonoBehaviour
{

// Variables for basic movement, both public so I can update inside of unity 
    [SerializeField] public float jumpHeight = 5f;
    [SerializeField] public float moveSpeed = 5f;
// Getting access to Rigidbody2D
    private Rigidbody2D bodyRigid;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
     private float wallJumpCooldown;

// Boolean variable for flip function to know if we are facing right
    private bool facesRight = true;

// Variables for sounds and accesing audio
private AudioSource audioPlayer;
public AudioClip jumpSound;
public AudioClip runningSound;

// Variable for accesing animator
    private Animator animate;
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

        horizontalInput = Input.GetAxisRaw("Horizontal");
        // bodyRigid.velocity = new Vector2(horizontalInput*moveSpeed, bodyRigid.velocity.y);
        animate.SetBool("isRunning", horizontalInput !=0);

        if(horizontalInput>0 && !facesRight || horizontalInput <0 && facesRight){
            Flip();
        }

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

    void Jump(){
        if(IsGround()){
            bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
            animate.SetTrigger("Jump");
             PlaySound(jumpSound);
        }else if(IsOnWall() && !IsGround()){
            if(horizontalInput == 0){
               bodyRigid.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10,0);
               Flip();

            } else{
                PlaySound(jumpSound);
                 bodyRigid.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3,6);
            }
            wallJumpCooldown =0;
       
        }
        

    }

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
    private bool IsGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    } 

    private bool IsOnWall(){    
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    } 

}

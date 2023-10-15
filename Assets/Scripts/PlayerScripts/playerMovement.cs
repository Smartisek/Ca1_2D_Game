using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class playerMovement : MonoBehaviour
{

// Variables for basic movement, both public so I can update inside of unity 
    public float jumpHeight = 5f;
    public float moveSpeed = 5f;
// Getting access to Rigidbody2D
    private Rigidbody2D bodyRigid;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;

// Boolean variable for flip function to know if we are facing right
    private bool facesRight = true;

// Variable for checking if player is on the ground 
    private bool grounded;
   

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

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        bodyRigid.velocity = new Vector2(horizontalInput*moveSpeed, bodyRigid.velocity.y);
        animate.SetBool("isRunning", horizontalInput !=0);

        if(horizontalInput>0 && !facesRight || horizontalInput <0 && facesRight){
            Flip();
        }

        if(horizontalInput !=0 && grounded){
            PlaySound(runningSound);
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

    }
    
    private void OnCollisionEnter2D(Collision2D other){
            // if(other.gameObject.CompareTag("IsGrounded")){
            //     grounded = true;
            // }
    }

    void Jump(){
        bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
        animate.SetTrigger("Jump");
        grounded = false;
        PlaySound(jumpSound);
        

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

    private bool IsGround(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    } 
}

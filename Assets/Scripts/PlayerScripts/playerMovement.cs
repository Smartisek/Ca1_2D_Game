using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

// Variables for basic movement, both public so I can update inside of unity 
    public float jumpHeight = 5f;
    public float moveSpeed = 5f;
// Getting access to Rigidbody2D
    private Rigidbody2D bodyRigid;

// Boolean variable for flip function to know if we are facing right
    private bool facesRight = true;

    private Animator animate;
    // Start is called before the first frame update
    void Awake()
    {
     bodyRigid = GetComponent<Rigidbody2D>();
     animate = GetComponent<Animator>();

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
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

    }
    

    void Jump(){
        bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
        animate.SetTrigger("Jump");
    }

    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facesRight = !facesRight;

    }
}

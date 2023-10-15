using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

// Variables for basic movement, both public so I can update inside of unity 
    public float jumpHeight = 5f;
    public float moveSpeed = 5f;
    private Rigidbody2D bodyRigid;

    // Start is called before the first frame update
    void Awake()
    {
     bodyRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        bodyRigid.velocity = new Vector2(horizontalInput*moveSpeed, bodyRigid.velocity.y);
        
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

    }
    

    void Jump(){
        bodyRigid.velocity = new Vector2(bodyRigid.velocity.x, jumpHeight);
    }
}

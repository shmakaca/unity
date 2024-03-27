using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    //var for movement and camera*****************
    [SerializeField] private int playerSpeed ;
    public Transform orientation;
    Vector3 moveDirection;
    public Rigidbody rb;

    float horizontal;
    float vertical;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag;
     
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplyer;
    bool readyToJump =true ;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;




    }
     
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && grounded)
        {
            playerSpeed = 15;


        }
        else
        {
            playerSpeed = 6;
        }
        MyInput();
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //drag(limit slleppery movement speed)
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        if(grounded && Input.GetKey(KeyCode.Space) && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
        SpeedControl();

    }
    private void FixedUpdate()
    {
        Moveplayer();
    }

    private void MyInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    } //gets input
    private void Moveplayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;
        if(grounded)
        {
            rb.AddForce(moveDirection * playerSpeed * 10, ForceMode.Force);

        }else if(!grounded)
        {
            rb.AddForce(moveDirection * playerSpeed * 10 *airMultiplyer , ForceMode.Force);
        }
        

    }//player movement(forces)
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x , 0f , rb.velocity.y);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }//jumping
    private void ResetJump()
    {
        readyToJump = true;

    }//resets your jump
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x ,0f ,rb.velocity.z);
        if (flatVel.magnitude > playerSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedVel.x ,rb.velocity.y , limitedVel.z);
        }
    }//limit player speed to playerspeed

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMoving : MonoBehaviour

{
    // Animation 

    Animator anim;

    // movement
    public CharacterController controller;

    public float speed = 6f;

    // cam 

    public float turnSmoothTime = 0.1f;

    public float turnSmoothVelocity;

    public Transform cam;

   // jump

    public float jumpHeight = 3f;

    // gravity 

    Vector3 velocity;

    public float gravity = -9.80f;

    // variables needed for groundcheck and use to adjust gravity and stop it from accelerating all the time.
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // push

    public float pushPower = 5.0f;

    // function for when the controller touches a collider from another object

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

      //  direction of the controlling when hitting an object
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // calculate push direction from move , we only push to the sides 

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();

        ResetSpeed();

        Move();

        Jump();

        // Animation

        //float velocity_Z = Vector3.Dot(moveDir.normalized, transform.forward);
        //float velocity_X = Vector3.Dot(moveDir.normalized, transform.right);

        //anim.SetFloat("velocityZ", velocity_Z, 0.1f, Time.deltaTime);
        //anim.SetFloat("velocityX", velocity_X, 0.1f, Time.deltaTime);

    }
    
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical"); //TODO: CHANGE TO NEW INPUT SYSTEM
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            Walk();
        }

        else if (direction.magnitude == 0f)
        {
            Idle();
        }
        // gravity 
        velocity.y += gravity * Time.deltaTime;

        // free fall equation
        controller.Move(velocity * Time.deltaTime);
    }

    private void ResetSpeed()
    {
        // resetting velocity 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void CheckGrounded()
    {
        // retunr true if the sphere's position created using a radious of 0.4 collides with anything identified as ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void Walk()
    {
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        anim.SetBool("Jump", false);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0);
        anim.SetBool("Jump", false);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetBool("Jump", true);
        }
    }
}

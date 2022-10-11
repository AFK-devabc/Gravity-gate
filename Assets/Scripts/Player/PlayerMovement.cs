using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float x_axisGravity;
    [SerializeField] private float y_axisGravity;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D body;

    float HorizontalInput;

    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;

    private Animator animator;
    public bool IsTeleportable { get;set; }
    private float ZrotationAngle;
    float sinRotation;
    float cosRotation;
    private bool isGrounded = false;
   
    public GameObject Boost;

    Vector2 gravity;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, groundLayer);


        animator.SetBool("isgrounded", isGrounded);

        HorizontalInput = Input.GetAxis("Horizontal");
        if (HorizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (HorizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }




        ZrotationAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        sinRotation = MathF.Sin(ZrotationAngle);
        cosRotation = MathF.Cos(ZrotationAngle);

        GravityControl();



        body.velocity = new Vector2(HorizontalInput*Speed * cosRotation, HorizontalInput * Speed * sinRotation )        // Left, right move (cos, sin)
            + new Vector2(-sinRotation * body.velocity.x, cosRotation * body.velocity.y)                                //Jumping move      (-sin, cos)
            + gravity* Time.deltaTime ;                                                                                 //gravity           (sin, -cos)
        //Debug.Log(cosRotation);
        //Debug.Log(-body.velocity.y - jumpPower * 0.8f);

        if (Input.GetKey(KeyCode.Space) )
            Jump();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            body.velocity = new Vector2(HorizontalInput * Speed * cosRotation
                - jumpPower * sinRotation
                , (HorizontalInput * Speed + body.velocity.y) * sinRotation
                + jumpPower * cosRotation);
        }
    }


    private void GravityControl()
    {
       
        //gravity effects when the player rotate z axis
        // G = (sin, -cos)
        gravity = new Vector2(x_axisGravity * sinRotation  ,- y_axisGravity * cosRotation );
    }

    private void CreateLandingAnimation()
    {
        Boost = Instantiate(Resources.Load("Prefabs/LandingParticle"), groundCheck.position, transform.rotation) as GameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 0.8f*jumpPower)
           CreateLandingAnimation();
    }
}

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float x_axisGravity;
    [SerializeField] private float y_axisGravity;
    private Rigidbody2D body;

    float HorizontalInput;

    private BoxCollider2D boxCollider;
    public bool IsTeleportable { get;set; }
    private float ZrotationAngle;
    float sinRotation;
    float cosRotation;

    Vector2 gravity;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
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
        if (Input.GetKey(KeyCode.Space) )
            Jump();
    }

    private void Jump()
    {
        if (IsGrounded())
            body.velocity = new Vector2(HorizontalInput * Speed * cosRotation
                - jumpPower * sinRotation
                , (HorizontalInput * Speed + body.velocity.y) * sinRotation
                + jumpPower * cosRotation) ;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(sinRotation, -cosRotation), 0.1f, groundLayer);  
        return raycastHit2D.collider != null;
    }

    private void GravityControl()
    {
       
        //gravity effects when the player rotate z axis
        // G = (sin, -cos)
        gravity = new Vector2(x_axisGravity * sinRotation  ,- y_axisGravity * cosRotation );
    }

}

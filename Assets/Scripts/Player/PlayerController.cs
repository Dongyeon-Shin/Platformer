using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float movePower;
    [SerializeField]
    private float jumpPower;
    private bool isGround;
    private Animator animator;
    private new Rigidbody2D rigidbody;
    private Vector2 inputDir;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    LayerMask grounLayer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (rigidbody.velocity.y < 0)
        {
            animator.SetBool("IsSoaring", false);
        }
        else if (rigidbody.velocity.y == 0 && isGround)
        {
            animator.SetBool("IsFloating", false);
        }
        Move();
        
    }

    //private void FixedUpdate()
    //{
    //    GroundCheck();
    //}

    private void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        animator.SetFloat("MoveSpeed", Mathf.Abs(inputDir.x));
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGround)
            Jump();
    }

    private void Move()
    {
        if (inputDir.x < 0 && rigidbody.velocity.x > -maxSpeed)
        {
            spriteRenderer.flipX = true;
            rigidbody.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        }
        else if (inputDir.x > 0 && rigidbody.velocity.x < maxSpeed)
        {
            spriteRenderer.flipX = false;
            rigidbody.AddForce(Vector2.right * inputDir.x * movePower, ForceMode2D.Force);
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetBool("IsSoaring", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGround = true;
        animator.SetBool("IsFloating", false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
        animator.SetBool("IsFloating", true);
    }
    //private void GroundCheck()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, grounLayer);
    //    Debug.DrawRay(transform.position, new Vector3( hit.point.x, hit.point.y, 0) - transform.position, Color.red);
    //    if (hit.collider != null)
    //    {
    //        Debug.Log(hit.collider.gameObject.name);
    //        isGround = true;
    //        animator.SetBool("IsFloating", true);
    //    }
    //    else
    //    {
    //        isGround = false;
    //        animator.SetBool("IsFloating", false);
    //    }
    //}
}

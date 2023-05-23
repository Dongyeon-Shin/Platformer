using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private LayerMask groundMask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
        if (!IsGroundExist())
        {
            Turn();
        }
    }
    public void Move()
    {
        rigidbody.velocity = new Vector2(transform.right.x * -moveSpeed, rigidbody.velocity.y);
    }
    public void Turn()
    {
        transform.Rotate(Vector3.up, 180);
    }
    private bool IsGroundExist()
    {
        return Physics2D.Raycast(groundCheckPoint.position, Vector2.down, 1f, groundMask);
    }
}

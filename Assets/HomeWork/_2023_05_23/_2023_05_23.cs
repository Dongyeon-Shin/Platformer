using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class _2023_05_23 : MonoBehaviour
{
    private bool isFloating;
    private Rigidbody2D rigidbody;
    [SerializeField]
    private Transform footTransform;
    private Vector2 moveDirection;
    [SerializeField]
    private LayerMask groundMask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        footTransform = transform.GetChild(1).GetComponent<Transform>();
    }
    private void Update()
    {
        Move();
        isFloating = !CheckFlightStatus();
    }
    private void Move()
    {
        rigidbody.AddForce(moveDirection.x * 2.5f * Vector2.right, ForceMode2D.Force);
    }
    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    private void Jump()
    {
        if (!isFloating)
        {
            rigidbody.AddForce(Vector2.up * 11f, ForceMode2D.Impulse);
        }
    }
    private void OnJump(InputValue value)
    {
        Jump();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    isFloating = false;
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    isFloating = true;
    //}
    private bool CheckFlightStatus()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y + 0.1f, groundMask);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2023_05_23_Monster : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Transform cliffSensor;
    [SerializeField]
    private LayerMask groundMask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cliffSensor = transform.GetChild(1).GetComponent<Transform>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (DetactCliff())
        {
            transform.Rotate(Vector3.up, 180);
        }
        rigidbody.velocity = new Vector2(transform.right.x * 2f, rigidbody.velocity.y);
    }
    private bool DetactCliff()
    {
        return !Physics2D.Raycast(cliffSensor.position, Vector2.down, transform.localScale.y + 0.1f, groundMask);
    }
}

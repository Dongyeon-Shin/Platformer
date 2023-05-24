using BeeState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2023_05_24 : MonoBehaviour
{
    public enum State
    {
        Idle,
        Trace,
        Return,
        Attack,
        Size
    }
    private Rigidbody2D rigidbody;
    private Transform cliffSensor;
    [SerializeField]
    private LayerMask groundMask;
    public Transform returnArea;
    private StateBase<_2023_05_24>[] states;
    public Transform player;
    int currentStateIndex;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        cliffSensor = transform.GetChild(1).GetComponent<Transform>();
        states = new StateBase<_2023_05_24>[(int)State.Size];
        states[(int)State.Idle] = new EnemyState.IdleState(this);

    }
    private void Start()
    {
        currentStateIndex = (int)State.Idle;
    }
    private void Update()
    {
        states[currentStateIndex].Update();
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
    public void ChangeState(int stateIndex)
    {
        currentStateIndex = stateIndex;
    }
}
namespace EnemyState
{
    public class IdleState : StateBase<_2023_05_24>
    {
        public IdleState(_2023_05_24 owner) : base(owner)
        {
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        public override void Setup()
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            if (Vector2.Distance(owner.transform.position, owner.player.position) < 4f)
            {
                owner.ChangeState(1);
            }
        }
    }
}

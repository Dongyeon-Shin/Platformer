using BeeState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bee : MonoBehaviour
{
    public enum State
    {
        Idle,
        Trace,
        Return,
        Attack,
        Patrol,
        Size // size는 무조건 열거형의 맨 뒤에. 열거형의 개수를 사용하기 위해
    }
    public Transform player;
    [SerializeField]
    public State currentState;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public float attackRange;
    [SerializeField]
    public float detectRange;
    public Vector3 returnPositon;
    public Transform[] patrolPoints;
    public int patrolIndex = 0;
    private StateBase<Bee>[] states;
    private void Awake()
    {
        states = new StateBase<Bee>[(int)State.Size];
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Trace] = new TraceState(this);
        states[(int)State.Return] = new ReturnState(this);
        states[(int)State.Attack] = new AttackState(this);
        states[(int)State.Patrol] = new PatrolState(this);
    }
    private void Start()
    {
        currentState = State.Idle;
        states[(int)State.Idle].Enter();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        returnPositon = transform.position;
    }
    private void Update()
    {
        states[(int)currentState].Update();
        //switch (currentState)
        //{
        //    case State.Idle:
        //        IdleUpdate();
        //        break;
        //    case State.Trace:
        //        TraceUpdate();
        //        break;
        //    case State.Attack:
        //        AttackUpdate();
        //        break;
        //    case State.Return:
        //        ReturnUpdate();
        //        break;
        //    case State.Patrol:
        //        PatrolUpdate();
        //        break;
        //}
    }
    float idleTime = 0;
    private void IdleUpdate()
    {
        if (idleTime > 2)
        {
            idleTime = 0;
            currentState = State.Patrol;
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
        idleTime += Time.deltaTime;
        if (Vector2.Distance(player.position, transform.position) < detectRange)
        {
            currentState = State.Trace;
        }
    }
    private void TraceUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        if (Vector2.Distance(player.position, transform.position) > detectRange)
        {
            currentState = State.Return;
        }
        else if (Vector2.Distance(player.position, transform.position) < attackRange)
        {
            currentState = State.Attack;
        }
    }
    private void ReturnUpdate()
    {
        Vector2 direrction = (returnPositon - transform.position).normalized;
        transform.Translate(direrction * moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, returnPositon) < 0.02f)
        {
            currentState = State.Idle;
        }
        else if (Vector2.Distance(player.position, transform.position) < detectRange)
        {
            currentState = State.Trace;
        }
    }
    float lastAttackTime = 0;
    private void AttackUpdate()
    {
        if (lastAttackTime > 1)
        {
            Debug.Log("공격함수 실행");
            lastAttackTime = 0;
        }
        lastAttackTime += Time.deltaTime;
        if ( Vector2.Distance(player.position, transform.position) > attackRange)
        {
            currentState = State.Trace;
        }
    }
    private void PatrolUpdate()
    {
        Vector2 direrction = (patrolPoints[patrolIndex].position - transform.position).normalized;
        transform.Translate(direrction * moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.02f)
        {
            currentState = State.Idle;
        }
        else if (Vector2.Distance(player.position, transform.position) < attackRange)
        {
            currentState = State.Trace;
        }
    }
    public void ChangeState(State state)
    {
        currentState = state;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
namespace BeeState
{
    public class IdleState : StateBase<Bee>
    {
        private float idleTime;
        public IdleState(Bee owner) : base(owner)
        {
        }

        public override void Setup()
        {

        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if (idleTime > 2)
            {
                idleTime = 0;
                owner.currentState = Bee.State.Patrol;
            }
            idleTime += Time.deltaTime;
            if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.detectRange)
            {
                owner.currentState = Bee.State.Trace;
            }
        }

        public override void Exit()
        {

        }
    }
    public class TraceState : StateBase<Bee>
    {
        public TraceState(Bee owner) : base(owner)
        {

        }

        public override void Setup()
        {

        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            Vector2 direction = (owner.player.position - owner.transform.position).normalized;
            owner.transform.Translate(direction * owner.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(owner.player.position, owner.transform.position) > owner.detectRange)
            {
                owner.currentState = Bee.State.Return;
            }
            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.attackRange)
            {
                owner.currentState = Bee.State.Attack;
            }
        }

        public override void Exit()
        {

        }
    }
    public class ReturnState : StateBase<Bee>
    {
        public ReturnState(Bee owner) : base(owner)
        {

        }

        public override void Setup()
        {

        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            Vector2 direrction = (owner.returnPositon - owner.transform.position).normalized;
            owner.transform.Translate(direrction * owner.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(owner.transform.position, owner.returnPositon) < 0.02f)
            {
                owner.currentState = Bee.State.Idle;
            }
            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.detectRange)
            {
                owner.currentState = Bee.State.Trace;
            }
        }

        public override void Exit()
        {

        }
    }
    public class AttackState : StateBase<Bee>
    {
        private float lastAttackTime = 0;
        public AttackState(Bee owner) : base(owner)
        {

        }

        public override void Setup()
        {

        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if (lastAttackTime > 1)
            {
                Debug.Log("공격함수 실행");
                lastAttackTime = 0;
            }
            lastAttackTime += Time.deltaTime;
            if (Vector2.Distance(owner.player.position, owner.transform.position) > owner.attackRange)
            {
                owner.currentState = Bee.State.Trace;
            }
        }

        public override void Exit()
        {

        }
    }
    public class PatrolState : StateBase<Bee>
    {
        public PatrolState(Bee owner) : base(owner)
        {

        }

        public override void Setup()
        {

        }

        public override void Enter()
        {
            owner.patrolIndex = (owner.patrolIndex + 1) % owner.patrolPoints.Length;
        }

        public override void Update()
        {
            Vector2 direrction = (owner.patrolPoints[owner.patrolIndex].position - owner.transform.position).normalized;
            owner.transform.Translate(direrction * owner.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(owner.transform.position, owner.patrolPoints[owner.patrolIndex].position) < 0.02f)
            {
                owner.currentState = Bee.State.Idle;
            }
            else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.attackRange)
            {
                owner.currentState = Bee.State.Trace;
            }
        }

        public override void Exit()
        {

        }
    }
}

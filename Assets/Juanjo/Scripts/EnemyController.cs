using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    [SerializeField] private float rangoVision;
    [SerializeField] private float anguloVision;
    [SerializeField] private float attackDistance;
    [SerializeField] private LayerMask queEsTarget;
    [SerializeField] private LayerMask queEsObstaculo;

    private State<EnemyController> currentState;
    private NavMeshAgent agent;
    private Transform target;
    private Vector3 ultimaPosicionConocida;
    private SurprisedState surprisedState;
    private AlertState alertState;

    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    #region getters & setters
    public NavMeshAgent Agent { get => agent; }
    public float RangoVision { get => rangoVision; }
    public LayerMask QueEsTarget { get => queEsTarget; }
    public LayerMask QueEsObstaculo { get => queEsObstaculo;}
    public float AnguloVision { get => anguloVision; }
    public PatrolState PatrolState { get => patrolState; }
    public ChaseState ChaseState { get => chaseState;  }
    public AttackState AttackState { get => attackState;}
    public AlertState AlertState { get => alertState; }
    public SurprisedState SurprisedState { get => surprisedState; }
    public Transform Target { get => target; set => target = value; }
    public float AttackDistance { get => attackDistance; }
    public Vector3 UltimaPosicionConocida { get => ultimaPosicionConocida; set => ultimaPosicionConocida = value; }
    #endregion

    private void Awake()
    {
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();
        surprisedState = GetComponent<SurprisedState>();
        alertState = GetComponent<AlertState>();
        agent = GetComponent<NavMeshAgent>();

        ChangeState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.OnUpdateState();
        }
    }
    public void ChangeState(State<EnemyController> newState)
    {
        if(currentState != null && currentState != newState)
        {
            currentState.OnExitState();
        }
        currentState = newState; //Mi estado actual pasa a ser el nuevo.
        currentState.OnEnterState(this);
    }
}

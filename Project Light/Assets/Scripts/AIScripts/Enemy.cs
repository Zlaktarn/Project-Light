using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum AIType
    {
        FastAI,
        SlowAI
    }

public class Enemy : MonoBehaviour
{
    public StateMachine_AI StateMachine => GetComponent<StateMachine_AI>();
    public GameObject Target {get; private set;}
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float chaseSpeed = 6f;
    public float wanderSpeed = 2f;
    public float chargeSpeed = 12f;
    public float attackRange = 10f;
    private bool gotHit = false;
    public bool smashAttacking = false;
    private float soundRange = 10f;
    private GameObject[] neighbors;
    private GameObject closestNeighbor;
    private GameObject player;
    public AIType currentAI;

    [HideInInspector]
    public int WanderState = 0;
    [HideInInspector]
    public int ChaseState = 1;
    [HideInInspector]
    public int AttackState = 2;

    void Awake()
    {
        if (currentAI == AIType.FastAI)
            InitializeFastAIStateMachine();
        else if(currentAI == AIType.SlowAI)
            InitializeSlowAIStateMachine();

        neighbors = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void InitializeFastAIStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(AttackState), new AttackState(this)}
        };

        GetComponent<StateMachine_AI>().SetStates(states);
    }

    private void InitializeSlowAIStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(SlowAttackState), new SlowAttackState(this)}
        };

        GetComponent<StateMachine_AI>().SetStates(states);
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    public bool GotHit()
    {
        return gotHit;
    }
    
    public void SetHit(bool yes)
    {
        gotHit = yes;
    }

    public void SetRange(float range)
    {
        soundRange = range;
    }

    public float GetRange()
    {
        return soundRange;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject, 1);
        }
    }

    public bool NeighborTooClose(float distance)
    {
        foreach(GameObject go in neighbors)
        {
            if(go != gameObject)
            {
                if(Vector3.Distance(go.transform.position, transform.position) < distance)
                {
                    closestNeighbor = go;
                    return true;
                }
            }
        }

        return false;
    }

    public bool PlayerTooClose()
    {
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < GetRange())
        {
            SetTarget(GameObject.FindGameObjectWithTag("Player"));
            return true;
        }
        return false;
    }

    public float GetAcceleration(float start, float end, float distance)
    {
        float accel = (Mathf.Sqrt(end) - Mathf.Sqrt(start))/2/distance;
        return accel;
    }

    public GameObject GetNeighbor()
    {
        return closestNeighbor;
    }
}

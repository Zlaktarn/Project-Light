using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public StateMachine_AI StateMachine => GetComponent<StateMachine_AI>();
    public GameObject Target {get; private set;}
    public NavMeshAgent Agent;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public bool navMesh;

    void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(AttackState), new AttackState(this)}
        };

        GetComponent<StateMachine_AI>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        Target = target;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject, 1);
        }
    }
}

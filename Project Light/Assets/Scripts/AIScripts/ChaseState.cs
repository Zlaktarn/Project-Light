using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private Enemy enemy;
    private float aggroRange = 30f;
    private float rotationSpeed = 2.5f;
    private float fixRotation = 5f;
    private float originalRotation = 2.5f;
    private float closeAngle = 10f;
    private bool move = true;
    private Vector3 direction;
    private Quaternion desiredRotation;

    public ChaseState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
    }

    public override Type Tick()
    {
        if(enemy.currentAI == AIType.FastAI)
            rotationSpeed = 2.5f;
        else if(enemy.currentAI == AIType.SlowAI)
            rotationSpeed = 5f;

        enemy.SetHit(false);

        if (enemy.Target == null)
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);

        if (Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange)
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);

        if(GetDistanceToPlayer() > 20f && IsPathBlocked())
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);

        if (!enemy.NeighborTooClose(2f))
            NormalTick();  
        else
            AvoidNieghbor();

        if (Vector3.Distance(transform.position, enemy.Target.transform.position) < enemy.attackRange && CloseEnoughRotation())
        {
            rotationSpeed = originalRotation;
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.AttackState);
        }

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) <= enemy.attackRange && !CloseEnoughRotation())
            rotationSpeed = fixRotation;

        return null;
    }

    private void AvoidNieghbor()
    {
        direction = Vector3.Normalize(enemy.GetNeighbor().transform.position - transform.position);
        direction = new Vector3(-direction.x, 0f, -direction.z);
        desiredRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * enemy.chaseSpeed);
    }

    private void NormalTick()
    {
        direction = Vector3.Normalize(enemy.Target.transform.position - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.forward * enemy.chaseSpeed * Time.deltaTime);
    }

    private bool CloseEnoughRotation()
    {
        if(Quaternion.Angle(transform.rotation, desiredRotation) <= closeAngle)
            return true;
        return false;
    }

    private float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, enemy.Target.transform.position);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        if(Physics.SphereCast(ray, 0.5f, 5.0f, environmentLayer))
            return true;
        return false;
    }
}

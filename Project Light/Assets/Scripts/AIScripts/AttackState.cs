using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private Enemy enemy;
    private Vector3 tempTargetTransform;
    private Vector3 tempAITransform;
    private float speed = 3f;
    private float aggroRange = 35f;
    private float resetRange = 25f;
    private float stunnedTimer = 0;
    private float stunnedLimit = 2f;
    private Vector3 direction;
    private Quaternion desiredRotation;

    public AttackState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
        reset = false;
        init = true;
    }

    public override Type Tick()
    {
        //gameObject.GetComponent<AIChargeAttack>().attacking = true;

        if (init)
        {
            tempAITransform = transform.position;
            tempTargetTransform = enemy.Target.transform.position;
            init = false;
        }

        if(enemy.Target == null)
        {
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);
        }

        direction = Vector3.Normalize(tempTargetTransform - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);

        if(Vector3.Distance(tempAITransform, transform.position) >= resetRange)
            reset = true;

        if (reset)
        {
            ResetBools();
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 0.2f);
            transform.Translate(Vector3.forward * Time.deltaTime * enemy.chargeSpeed);
        }

        if (IsPathBlocked() && !IsTargetTooClose())
        {
            ResetBools();
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);
        }

        if (IsForwardBlocked() && !IsTargetTooClose())
        {
            ResetBools();
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
        }

        if(IsCloseForwardBlocked() && IsTargetTooClose())
        {
            enemy.chargeSpeed = 0;
            return CheckIfTargetBehind();
        }

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange)
        {
            ResetBools();
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
        }
        return null;
    }

    private void ResetBools()
    {
        reset = false;
        init = true;
        enemy.chargeSpeed = 10f;
    }

    private Type CheckIfTargetBehind()
    {
        var heading = enemy.Target.transform.position - transform.position;
        var dot = Vector3.Dot(heading, transform.forward);
        if(dot < 0.1f)
        {
            ResetBools();
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
        }

        return null;
    }

    private void SoftResetBools()
    {
        reset = true;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.SphereCast(ray, 0.5f, 5.0f, environmentLayer))
            return true;
        return false;
    }

    private bool IsCloseForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.SphereCast(ray, 0.5f, 0.5f, environmentLayer))
            return true;
        return false;
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        if(Physics.SphereCast(ray, 0.5f, 5.0f, environmentLayer))
            return true;
        return false;
    }

    private bool IsTargetTooClose()
    {
        var dist = Vector3.Distance(transform.position, enemy.Target.transform.position);
        if(dist < 6f)
            return true;
        return false;
    }
}

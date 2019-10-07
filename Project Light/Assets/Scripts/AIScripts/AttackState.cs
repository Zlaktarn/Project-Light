using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private Enemy enemy;
    private Vector3 tempTargetTransform;
    private float lungeForce = 0.3f;
    private Rigidbody rb;
    private float speed = 3f;
    private float fleeSpeed = 5f;
    private float aggroRange = 10f;
    private float resetRange = 2f;
    private Vector3 direction;
    private Quaternion desiredRotation;

    public AttackState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
        rb = gameObject.GetComponent<Rigidbody>();
        reset = false;
        init = true;
    }

    public override Type Tick()
    {
        if(enemy.Target == null)
            return typeof(WanderState);

        if (IsForwardBlocked())
        {
            ResetBools();
            return typeof(ChaseState);
        }
            

        direction = Vector3.Normalize(enemy.Target.transform.position - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);

        if (init)
        {
            tempTargetTransform = enemy.Target.transform.position;
            init = false;
        }

        if(Vector3.Distance(transform.position, tempTargetTransform) <= resetRange)
            reset = true;

        if (reset)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * fleeSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 1.5f);
            rb.AddForce((tempTargetTransform - transform.position).normalized * lungeForce, ForceMode.VelocityChange);
        }

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange)
        {
            ResetBools();
            return typeof(ChaseState);
        }
        return null;
    }

    private void ResetBools()
    {
        reset = false;
        init = true;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.SphereCast(ray, 0.5f, 5.0f, environmentLayer))
            return true;
        return false;
    }
}

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
    private bool reset, init;
    private float speed = 3f;
    private float aggroRange = 10f;
    private float resetRange = 2f;
    private Vector3 direction;

    public AttackState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
        rb = gameObject.GetComponent<Rigidbody>();
        reset = false;
        init = true;
    }

    public override Type Tick()
    {
        direction = Vector3.Normalize(enemy.Target.transform.position - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);

        if (init)
        {
            tempTargetTransform = enemy.Target.transform.position;
            init = false;
        }

        if(enemy.Target == null)
            return typeof(WanderState);

        if(Vector3.Distance(transform.position, tempTargetTransform) <= resetRange)
            reset = true;

        if(reset)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        else
            rb.AddForce((tempTargetTransform - transform.position).normalized * lungeForce, ForceMode.VelocityChange);

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange || IsPathBlocked())
        {
            init = true;
            reset = false;
            return typeof(ChaseState);
        }

        return null;
    }

    
    private bool IsPathBlocked()
    {
        RaycastHit hit;
        var pos = transform.position;
        if(Physics.SphereCast(pos, 0.5f, direction, out hit, 5.0f))
            if(hit.collider.tag == "Environment")
                return true;

        return false;
    }
}

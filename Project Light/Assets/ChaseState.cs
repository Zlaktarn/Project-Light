using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private Enemy enemy;
    private float speed = 3f;
    private float attackRange = 10f;
    private float aggroRange = 20f;
    private float rotationSpeed = 2f;
    private Vector3 direction;
    private Quaternion desiredRotation;

    public ChaseState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
    }

    public override Type Tick()
    {
        if(enemy.Target == null)
            return typeof(WanderState);

        direction = Vector3.Normalize(enemy.Target.transform.position - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 1.5f);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) < attackRange)
            return typeof(AttackState);

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange || IsPathBlocked())
            return typeof(WanderState);

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

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
    private float rotationSpeed = 1.5f;
    private float closeAngle = 25f;
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

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) < attackRange && CloseEnoughRotation())
            return typeof(AttackState);

        if(Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange || IsPathBlocked())
            return typeof(WanderState);

        return null;
    }

    private bool CloseEnoughRotation()
    {
        if(Quaternion.Angle(transform.rotation, desiredRotation) <= closeAngle)
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class WanderState : BaseState
{
    private float rayDistance = 5.0f;
    private float stoppingDistance = 1.5f;
    private float rotationSpeed = 1.5f;
    private bool holdRotation = false;
    
    private Vector3 newPosition;
    private Vector3 rightOrLeft;
    private Vector3 destination;
    private Quaternion desiredRotation;
    private Vector3 direction;
    private Enemy enemy;
    private Color debugColor = Color.white;
    
    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    public WanderState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
    }

    public override Type Tick()
    {
        //gameObject.GetComponent<AIChargeAttack>().attacking = false;

        if (enemy.GotHit())
        {
            enemy.SetTarget(GameObject.FindGameObjectWithTag("Player"));
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
        }

        var targetToAggro = CheckForAggro();

        if (targetToAggro != null && !IsPathBlocked())
        {
            enemy.SetTarget(targetToAggro.gameObject);
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
        }

        if (enemy.PlayerTooClose() && !IsPathBlocked() && !IsForwardBlocked())
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);

        NormalTick();


        return null;
    }

    private void NormalTick()
    {
        var rayColor = IsPathBlocked() ? Color.red : Color.green;
        Debug.DrawRay(transform.position, direction * rayDistance, rayColor);

        if (NeedsDestination())
            GetDestination(false);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);

        if (IsForwardBlocked())
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.1f);
        else
            transform.Translate(Vector3.forward * Time.deltaTime * enemy.wanderSpeed);

        if(IsPathBlocked())
        {
            Debug.Log("Path blocked!");
            GetDestination(true);
        }
    }

    private bool NeedsDestination()
    {
       if (destination == Vector3.zero)
            return true;

       var distance = Vector3.Distance(transform.position, destination);
       if (distance <= stoppingDistance)
            return true;

       return false;
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, direction);
        if(Physics.SphereCast(ray, 0.5f, rayDistance, environmentLayer))
            return true;
        return false;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.SphereCast(ray, 0.5f, rayDistance, environmentLayer))
            return true;
        return false;
    }

    private void GetDestination(bool reverse)
    {
        if (!reverse)
        {
            newPosition = (transform.position + (transform.forward * 4f)) +
                               new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0f,
                                   UnityEngine.Random.Range(-2.5f, 2.5f));
        }
        else
        {
            if(UnityEngine.Random.Range(1, 2) == 2)
                rightOrLeft = transform.right;
            else
                rightOrLeft = -transform.right;
            newPosition = (transform.position + (rightOrLeft * 4f)) +
                               new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0f,
                                   UnityEngine.Random.Range(-2.5f, 2.5f));
        }

       destination = new Vector3(newPosition.x, 0f, newPosition.z);

       direction = Vector3.Normalize(destination - transform.position);
       direction = new Vector3(direction.x, 0f, direction.z);
       desiredRotation = Quaternion.LookRotation(direction);
    }

    private Transform CheckForAggro()
    {
        float aggroRadius = 10f;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, 3.0f, out hit, aggroRadius, playerLayer))
        {
            if (!IsPathBlocked())
            {
                var player = hit.collider.GetComponent<Transform>();
                return player;
            }
        } 
        return null;
    }

    // NavMesh Agent methods - may be useful in the future
    private void NavGetDestination()
    {
        float radius = 100;
        Vector3 testDirection = UnityEngine.Random.insideUnitSphere * radius;
        testDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(testDirection, out hit, radius, 3);
        Vector3 finalTestPosition = hit.position;

        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position, finalTestPosition, NavMesh.AllAreas, path))
            if(path.status == NavMeshPathStatus.PathComplete)
                destination = finalTestPosition;
            else
                NavGetDestination();
    }
}

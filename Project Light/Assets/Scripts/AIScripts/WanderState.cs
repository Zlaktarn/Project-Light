using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WanderState : BaseState
{
    private float rayDistance = 5.0f;
    private float stoppingDistance = 1.5f;
    private float speed = 1f;
    
    private readonly LayerMask layerMask;
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
        layerMask = LayerMask.NameToLayer("Wall");
    }

    public override Type Tick()
    {
        Debug.DrawRay(transform.position, direction * 15, debugColor);

        if (NeedsDestination())
            GetDestination();

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 1.5f);

        if (IsForwardBlocked())
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.2f);
        else
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        while(IsPathBlocked())
            GetDestination();

        var targetToAggro = CheckForAggro();

        if(targetToAggro != null)
        {
            enemy.SetTarget(targetToAggro.gameObject);
            return typeof(ChaseState);
        }
        return null;
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

    private bool IsForwardBlocked()
    {
        RaycastHit hit;
        var pos = transform.position;
        if(Physics.SphereCast(pos, 0.5f, transform.forward, out hit, rayDistance))
            if(hit.collider.tag == "Environment")
                return true;

        return false;
    }

    private bool IsPathBlocked()
    {
        RaycastHit hit;
        var pos = transform.position;
        if(Physics.SphereCast(pos, 0.5f, direction, out hit, rayDistance))
            if(hit.collider.tag == "Environment")
                return true;

        return false;
    }

    private void GetDestination()
    {
       Vector3 testPosition = (transform.position + (transform.forward * 4f)) +
                               new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0f,
                                   UnityEngine.Random.Range(-2.5f, 2.5f));

       destination = new Vector3(testPosition.x, 1f, testPosition.z);

       direction = Vector3.Normalize(destination - transform.position);
       direction = new Vector3(direction.x, 0f, direction.z);
       desiredRotation = Quaternion.LookRotation(direction);
    }

    private Transform CheckForAggro()
    {
       float aggroRadius = 10f;
        
       RaycastHit hit;
       var pos = transform.position;
       if (Physics.SphereCast(pos, 10f, direction, out hit, aggroRadius))
       {
           if (hit.collider.tag == "Player")
           {
               var player = hit.collider.gameObject;
               Debug.Log("player: " + player);
               return player.transform;
           }
       } 
       return null;
    }
}

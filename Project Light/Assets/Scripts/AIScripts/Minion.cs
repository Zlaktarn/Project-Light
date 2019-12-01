using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    // Wander Variables
    public Vector3 wanderTarget;
    private float wanderDistance = 20f;
    private bool foundWanderTarget = false;
    public float wanderSpeed = 2f;

    // Chase Variables
    private float soundRange = 15f;
    private float sightRange = 20f;
    private float aggroRange = 30f;
    public float chaseSpeed = 6f;
    private float aggroRadius = 3f;

    // Attack Variables
    public float attackRange = 8f;
    public float attackForce = 6f;
    public float downTime = 2f;
    private float downTimer = 0f;
    private float extraRotationSpeed = 7f;
    private float requiredMinAngle = 5f;
    private float chargeTimer = 0f;
    private float chargeLimit = 1.5f;
    private float smashTimer = 0f;
    public float swipeCooldown = 2f;
    private bool attacking;
    private GameObject attackCube;
    private GameObject spawnedCube;
    private Vector3 cubePos;
    private Vector3 chargeTarget;
    private Vector3 otherChargeTarget;
    private Quaternion cubeRot;

    // Global Variables
    private int allLayerMask = -1;
    private int environmentLayer = 1 << 10;
    private int playerLayer = 1 << 9;
    private float distanceToPlayer;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 directionToPlayer;
    private Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackCube = GameObject.FindGameObjectWithTag("SwipeCube");
        rb = GetComponent<Rigidbody>();
        if(agent.isStopped)
            agent.isStopped = false;

        if(!agent.enabled)
            agent.enabled = true;
    }

    void Update()
    {
        soundRange = player.GetComponent<MovementScript>().soundRange;

        directionToPlayer = transform.position - player.transform.position;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        chargeTimer += Time.deltaTime;
        downTimer += Time.deltaTime;
        smashTimer += Time.deltaTime;
    }

    // Basically acts as the GameObjects vision cone.
    private bool AgentVision()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Ray playerRay = new Ray(transform.position, directionToPlayer);
        if(Physics.SphereCast(ray, aggroRadius, sightRange, playerLayer))
            if(!Physics.SphereCast(playerRay, 0.5f, 5f, environmentLayer))
                return true;
        return false;
    }

    private bool NoPathForward()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, 4, environmentLayer))
            return true;
        return false;
    }

    private bool CloseEnoughRotation(Quaternion desiredRotation, float closeAngle)
    {
        if(Quaternion.Angle(transform.rotation, desiredRotation) <= closeAngle)
            return true;
        return false;
    }

    public void EnableAgent()
    {
        GetComponent<AIChargeAttack>().attacking = false;
        rb.isKinematic = true;
        if(!agent.enabled)
            agent.enabled = true;
        if(agent.isStopped)
            agent.isStopped = false;
    }

    [Task]
    public void IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, directionToPlayer);
        if(Physics.SphereCast(ray, 0.5f, 5f, environmentLayer))
            Task.current.Fail();
        else
            Task.current.Succeed();
    }

    // Finds random position in a sphere around the gameobject
    // then multiplies that by a determined distance.
    // Lastly it samples the position to not get something
    // that is out of bounds.
    [Task]
    public void FindWanderTarget()
    {

        if (!foundWanderTarget)
        {
            Vector3 randDirection = Random.insideUnitSphere * wanderDistance;
            randDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randDirection, out hit, wanderDistance, 1);
            wanderTarget = hit.position;
            foundWanderTarget = true;
        }

        if (foundWanderTarget)
            Task.current.Succeed();
    }

    // Walks to the aforementioned target
    [Task]
    public void WalkToWanderTarget()
    {
        if(AgentVision() || distanceToPlayer <= soundRange)
            Task.current.Succeed();

        if(agent.remainingDistance < 1f)
            Task.current.Succeed();

        foundWanderTarget = false;
        agent.speed = wanderSpeed;
        agent.SetDestination(wanderTarget);
    }

    [Task]
    public void CanHearPlayer()
    {
        if(distanceToPlayer <= soundRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    // The +10 on the sound range is so that it
    // doesn't instant lose aggro after charging.
    [Task]
    public void ChasePlayer()
    {
        if(distanceToPlayer <= attackRange && agent != null)
        {
            agent.enabled = false;
            Task.current.Succeed();
        }

        if(distanceToPlayer >= aggroRange)
            Task.current.Fail();

        if(!AgentVision() && distanceToPlayer > soundRange + 10)
            Task.current.Fail();

        agent.enabled = true;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.transform.position);
    }

    [Task]
    public void CanSeePlayer()
    {
        if(AgentVision())
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void IsInAttackRange()
    {
        if(distanceToPlayer <= attackRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    // Fixes the rotation quicker than usual so it aligns
    // with the player (so it doesn't miss).
    [Task]
    public void FixRotation()
    {
        attacking = true;
        chargeTimer = 0f;
        Vector3 lookRotation = Vector3.Normalize(player.transform.position - transform.position);
        lookRotation = new Vector3(lookRotation.x, 0, lookRotation.z);
        var desiredRotation = Quaternion.LookRotation(lookRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, extraRotationSpeed * Time.deltaTime);

        if(CloseEnoughRotation(desiredRotation, requiredMinAngle))
            Task.current.Succeed();

        if(distanceToPlayer >= aggroRange)
            Task.current.Fail();

        if(AgentVision() && distanceToPlayer > soundRange)
            Task.current.Fail();
    }

    [Task]
    public void Charge2()
    {
        if (Task.current.isStarting)
        {
            GetComponent<AIChargeAttack>().attacking = true;
            attacking = true;
            chargeTarget = transform.position + (transform.forward * 15);
            NavMeshHit hit;
            if(NavMesh.SamplePosition(chargeTarget, out hit, 1.0f, NavMesh.AllAreas))
                chargeTarget = hit.position;
        }

        if (agent != null)
        {
            agent.speed = 25;
            agent.SetDestination(chargeTarget); 
        }

        if (Vector3.Distance(transform.position, chargeTarget) <= 3f)
        {
            GetComponent<AIChargeAttack>().attacking = false;
            attacking = false;
            downTimer = 0f;
            Task.current.Succeed(); 
        }
    }

    [Task]
    public void WalkOutsideOfAttackRange()
    {
        if (Task.current.isStarting)
        {
            otherChargeTarget = (transform.position + (transform.forward * attackRange)) +
                               new Vector3(UnityEngine.Random.Range(-2.5f, 2.5f), 0f,
                                   UnityEngine.Random.Range(-2.5f, 2.5f));
            NavMeshHit hit;
            if(NavMesh.SamplePosition(otherChargeTarget, out hit, 1.0f, NavMesh.AllAreas))
                otherChargeTarget = hit.position;
        }

        agent.speed = chaseSpeed;
        agent.SetDestination(otherChargeTarget);

        if(Vector3.Distance(transform.position, otherChargeTarget) <= 3)
            Task.current.Succeed();
    }

    // Charges forward with a velocity change
    // Needs to paus navmesh agent or else its movement
    // overrides the charge. Resumes to stop the charge after set limit.
    [Task]
    public void Charge()
    {
        if(!agent.enabled)
            agent.enabled = true;

        if (Task.current.isStarting)
        {
            chargeTarget = transform.position + (transform.forward * 15);
        }

        if (NoPathForward())
        {
            EnableAgent();
            downTimer = 0f;
            Task.current.Succeed();
        }

        // GetComponent<AIChargeAttack>().attacking = true;
        
        if(!agent.isStopped)
            agent.isStopped = true;

        rb.isKinematic = false;
        rb.AddForce(transform.forward * (attackForce - Time.deltaTime), ForceMode.VelocityChange);
        if (Vector3.Distance(transform.position, chargeTarget) <= 3f)
        {
            EnableAgent();
            downTimer = 0f;
            Task.current.Succeed(); 
        }
    }

    // This is stupid but it works so don't touch.
    // And yes i've tried doing this in the above method - doesn't work.
    // I've also tried using the built in Wait method in panda BT - doesn't work.
    [Task]
    public void DownTime()
    {
        GetComponent<AIChargeAttack>().attacking = true;

        if(!agent.isStopped)
            agent.isStopped = true;

        rb.isKinematic = false;
        if(downTimer >= downTime)
        {
            EnableAgent();
            Task.current.Succeed();
        }
    }

    // Slams the ground when close enough causing an uninterruptable 
    // animation to occur that hits an area infront of the npc
    [Task]
    public void Slam()
    {
        if(agent.enabled)
            agent.enabled = false;

        if(Task.current.isStarting)
            attacking = true;

        if (attacking)
        {
            var cubeDir = transform.forward;
            cubeRot = transform.rotation;
            float distance = 1f;
            cubePos = transform.position + cubeDir * distance;

            spawnedCube = GameObject.Instantiate(attackCube, cubePos, cubeRot);
            spawnedCube.transform.localScale = new Vector3(1f, 0.8f, 1f);
            spawnedCube.GetComponent<AISwipeAttack>().enabled = true;
            spawnedCube.GetComponent<AISwipeAttack>().force = 50f;
            attacking = false;
        }

        if (spawnedCube == null)
        {
            smashTimer = 0;
            EnableAgent();
            Task.current.Succeed();
        } 
    }

    [Task]
    public void SmashIsNotOnCooldown()
    {
        if(smashTimer >= swipeCooldown)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }
}

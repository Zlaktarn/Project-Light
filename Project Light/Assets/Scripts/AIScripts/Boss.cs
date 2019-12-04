using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class Boss : MonoBehaviour
{
    // Roar Variables
    private int amountOfSummons = 2;
    private float summonRadius = 15f;
    private float roarCooldown = 45f;
    private float roarTimer = 0f;
    private float animationTimer = 0f;
    private float animationDuration = 2f;
    private float health = 51f;
    private float healthThreshold = 50f;
    private EnemyManager em;

    // Charge Variables
    private float chargeCooldown = 8f;
    private float chargeTimer = 0f;
    private float chargeMaxRange = 30f;
    private float chargeMinRange = 15f;
    private float chargeDistance = 35f;
    private float speedIncrease = 16f;
    private float rotationSpeed = 2f;
    private Quaternion chargeDirection;
    private Vector3 chargeTarget;

    // Smash Variables
    private bool smashAttacking = true;
    private float smashRange = 5f;
    private float smashScale = 3f;
    private float smashTimer = 0f;
    private float smashCooldown = 10f;
    private GameObject attackCube;
    private GameObject spawnedCube;
    private Vector3 cubePos;
    private Quaternion cubeRot;

    // Swipe Variables
    private bool swipeAttacking = true;
    private float swipeRange = 4f;
    private float swipeScale = 2f;
    private float swipeTimer = 0f;
    private float swipeCooldown = 1f;
    private GameObject swipeCube;
    private GameObject spawnedSwipeCube;
    private Vector3 swipePos;
    private Quaternion swipeRot;

    // Global Variables
    private bool attacking = false;
    private float currentSpeed;
    private int environmentLayer = 1 << 10;
    private float distanceToPlayer = 0f;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 directionToPlayer;
    private float distanceToStart = 0;
    private float distanceFromPlayerToStart = 0f;
    private Vector3 previousPosition;
    private Rigidbody rb;
    private Vector3 startPosition;
    private float aggroRange = 200f;

    // Animation
    private Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        m_Animator = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackCube = GameObject.FindGameObjectWithTag("AttackCube");
        swipeCube = GameObject.FindGameObjectWithTag("SwipeCube");
        em = GameObject.FindGameObjectWithTag("GM").GetComponent<EnemyManager>();
        rb = GetComponent<Rigidbody>();
        roarTimer = roarCooldown;
        chargeTimer = chargeCooldown;
        agent.enabled = true;
        agent.isStopped = false;
        attacking = false;
        agent.Warp(transform.position);
    }

    void FixedUpdate()
    {
        distanceFromPlayerToStart = Vector3.Distance(player.transform.position, startPosition);
        distanceToStart = Vector3.Distance(transform.position, startPosition);
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        roarTimer += Time.deltaTime;
        chargeTimer += Time.deltaTime;
        animationTimer += Time.deltaTime;
        smashTimer += Time.deltaTime;
        swipeTimer += Time.deltaTime;

        currentSpeed = Mathf.Lerp(currentSpeed, (transform.position - previousPosition).magnitude / Time.deltaTime, 0.75f);
        previousPosition = transform.position;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, 4, environmentLayer))
            return true;
        return false;
    }

    [Task]
    public bool IsCharging;

    [Task]
    public void HealthBelowThreshold()
    {
        if(health <= healthThreshold)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void RoarIsNotOnCooldown()
    {
        if(roarTimer >= roarCooldown)
        {
            animationTimer = 0f;
            Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }

    [Task]
    public void Roar()
    {
        m_Animator.SetBool("Roar", true);
        attacking = true;
        em.SpawnMinionsInRadius(transform.position, summonRadius, amountOfSummons);
        roarTimer = 0f;

        if(animationTimer >= animationDuration)
        {
            attacking = false;
            m_Animator.SetBool("Roar", false);
            Task.current.Succeed();
        }
    }

    [Task]
    public void IsInChargeRange()
    {
        if(distanceToPlayer <= chargeMaxRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void IsNotTooClose()
    {
        if(distanceToPlayer <= chargeMinRange)
            Task.current.Fail();
        else
            Task.current.Succeed();
    }

    [Task]
    public void ChargeIsNotOnCooldown()
    {
        if(chargeTimer >= chargeCooldown)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void FindChargePosition()
    {
        if (Task.current.isStarting)
        {
            var direction = Vector3.Normalize(player.transform.position - transform.position);
            direction = new Vector3(direction.x, 0f, direction.z);
            chargeDirection = Quaternion.LookRotation(direction);
            chargeTarget = player.transform.position + (transform.forward * chargeDistance);  
        }

        if(chargeDirection != null)
        {
            IsCharging = true;
            Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }

    [Task]
    public void Charge()
    {
        GetComponent<AIChargeAttack>().attacking = true;
        m_Animator.SetBool("Charge", true);
        attacking = true;
        agent.isStopped = true;
        rb.isKinematic = false;

        var desiredRotation = chargeDirection;
        transform.rotation = desiredRotation;
        transform.Translate(Vector3.forward * (speedIncrease) * Time.deltaTime);

        if (IsForwardBlocked())
        {
            m_Animator.SetBool("Charge", false);
            GetComponent<AIChargeAttack>().attacking = false;
            agent.enabled = true;
            agent.isStopped = false;
            rb.isKinematic = true;
            IsCharging = false;
            attacking = false;
            chargeTimer = 0f;
            Task.current.Fail();
        }

        if(Vector3.Distance(transform.position, chargeTarget) <= 8f)
        {
            m_Animator.SetBool("Charge", false);
            GetComponent<AIChargeAttack>().attacking = false;
            agent.enabled = true;
            agent.isStopped = false;
            rb.isKinematic = true;
            IsCharging = false;
            attacking = false;
            chargeTimer = 0f;
            Task.current.Succeed();
        }
    }

    [Task]
    public void IsInSmashRange()
    {
        if(distanceToPlayer <= smashRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void SmashIsNotOnCooldown()
    {
        if(smashTimer >= smashCooldown)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void Smash()
    {
        m_Animator.SetBool("Smash", true);
        if(Task.current.isStarting)
            smashAttacking = true;

        attacking = true;
        agent.enabled = false;

        if (smashAttacking)
        {
            var cubeDir = transform.forward;
            cubeRot = transform.rotation;
            float distance = smashScale + 2;
            cubePos = transform.position + cubeDir * distance;

            RaycastHit hit;
            var ray = transform.TransformDirection(Vector3.down);
            if (Physics.Raycast(player.transform.position, ray, out hit))
            {
                cubeRot.x = Quaternion.FromToRotation(Vector3.up, hit.normal).x;
                cubePos.y = player.transform.position.y;
            }

            spawnedCube = GameObject.Instantiate(attackCube, cubePos, cubeRot);
            spawnedCube.transform.localScale *= smashScale;
            spawnedCube.GetComponent<AISmashAttack>().enabled = true;
            smashAttacking = false;
        }

        if (spawnedCube != null)
            if (!spawnedCube.GetComponent<AISmashAttack>().attacking)
            {
                m_Animator.SetBool("Smash", false);
                agent.enabled = true;
                smashAttacking = true;
                attacking = false;
                smashTimer = 0f;
                Task.current.Succeed();
            } 
    }

    [Task]
    public void IsInSwipeRange()
    {
        if(distanceToPlayer <= swipeRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void SwipeIsNotOnCooldown()
    {
        if(swipeTimer >= swipeCooldown)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void Swipe()
    {
         m_Animator.SetBool("Swipe", true);
        if(Task.current.isStarting)
            swipeAttacking = true;

        attacking = true;
        agent.enabled = false;

        if (swipeAttacking)
        {
            var swipeDir = transform.forward;
            swipeRot = transform.rotation;
            float distance = 3;
            swipePos = transform.position + swipeDir * distance;

            spawnedSwipeCube = GameObject.Instantiate(swipeCube, swipePos, swipeRot);
            spawnedSwipeCube.transform.localScale = new Vector3(2,2,2);
            spawnedSwipeCube.GetComponent<AISwipeAttack>().force = 70f;
            spawnedSwipeCube.GetComponent<AISwipeAttack>().duration = 1f;
            spawnedSwipeCube.GetComponent<AISwipeAttack>().shieldDuration = 0.4f;
            spawnedSwipeCube.GetComponent<AISwipeAttack>().enabled = true;
            swipeAttacking = false;
        }

        if (spawnedSwipeCube == null)
        {
                m_Animator.SetBool("Swipe", false);
                agent.enabled = true;
                swipeAttacking = true;
                attacking = false;
                swipeTimer = 0f;
                Task.current.Succeed();
        }
    }

    [Task]
    public void IsOutsideOfAggroRange()
    {
        if(distanceToStart >= aggroRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void IsPlayerOutsideOfAggroRange()
    {
        if(distanceFromPlayerToStart >= aggroRange)
            Task.current.Succeed();
        else
            Task.current.Fail();
    }

    [Task]
    public void WalkBackToStart()
    {
        agent.enabled = true;
        agent.isStopped = false;

        if(Vector3.Distance(transform.position, startPosition) > 4)
            agent.SetDestination(startPosition);
        else
            Task.current.Fail();
        
    }

    [Task]
    public void ChasePlayer()
    {
        if(attacking)
            Task.current.Succeed();

        agent.enabled = true;
        agent.isStopped = false;

        agent.SetDestination(player.transform.position);
        Task.current.Succeed();
    }
}

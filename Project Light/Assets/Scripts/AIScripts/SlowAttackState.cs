using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAttackState : BaseState
{
    private Enemy enemy;
    private GameObject attackCube;
    private GameObject spawnedCube;
    private Vector3 cubePos;
    private Quaternion cubeRot;
    private GameObject player;
    private bool attacking = true;
    private float aggroRange = 20f;

    public SlowAttackState(Enemy theEnemy) : base(theEnemy.gameObject)
    {
        enemy = theEnemy;
        attackCube = GameObject.FindGameObjectWithTag("AttackCube");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override Type Tick()
    {
        if(enemy.Target == null)
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);

        if (Vector3.Distance(transform.position, enemy.Target.transform.position) > aggroRange)
        {
            ResetBools();
            return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.WanderState);
        }

        if (attacking)
        {
            var cubeDir = transform.forward;
            cubeRot = transform.rotation;
            float distance = 3f;
            cubePos = transform.position + cubeDir * distance;

            RaycastHit hit;
            var ray = transform.TransformDirection(Vector3.down);
            if (Physics.Raycast(player.transform.position, ray, out hit))
            {
                cubeRot.x = Quaternion.FromToRotation(Vector3.up, hit.normal).x;
                cubePos.y = hit.transform.position.y;
            }

            spawnedCube = GameObject.Instantiate(attackCube, cubePos, cubeRot);
            spawnedCube.GetComponent<AISmashAttack>().enabled = true;
            attacking = false;
        }

        if (spawnedCube != null)
            if (!spawnedCube.GetComponent<AISmashAttack>().attacking)
            {
                attacking = true;
                return gameObject.GetComponent<StateMachine_AI>().GetType(enemy.ChaseState);
            }

        return null;
    }

    private void ResetBools()
    {
        attacking = true;
    }
}

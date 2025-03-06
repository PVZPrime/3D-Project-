using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//https://www.youtube.com/watch?v=aNZw588BQBo
//https://www.youtube.com/watch?v=UjkSFoLxesw
namespace Enemy
{
    public class EnemyAiMelee : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform Player;
        private Animator anim;
        public LayerMask whatIsGround, whatIsPlayer;

        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        public float timeBetweenAttacks;
        bool alreadyAttacked;
        
        public int damageAmount;
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange, lookAtPlayer;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            anim.SetBool("walk", true);
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position , sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position , attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();

        }

        private void Patroling()
        {
            anim.SetBool("walk", true);
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position + new Vector3(0f, 1f, 0f) - walkPoint;
            if(distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        private void SearchWalkPoint()
        { 
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + 1f, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
            walkPointSet = true;
        }
        
        private void ChasePlayer()
        {
            anim.SetBool("walk", true);
            agent.SetDestination(Player.position);
        }

        private void AttackPlayer()
        {
            anim.SetBool("walk", false);
            agent.SetDestination(transform.position + new Vector3(0f, 1f, 0f));

            if(lookAtPlayer)transform.LookAt(Player);
            if(!alreadyAttacked)
            {
                anim.SetTrigger("attack");

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3 (0f, 1f, 0f), attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 1f, 0f), sightRange);
        }
        
    }
}
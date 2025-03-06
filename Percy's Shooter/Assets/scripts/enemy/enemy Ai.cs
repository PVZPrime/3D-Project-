using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//https://www.youtube.com/watch?v=aNZw588BQBo
//https://www.youtube.com/watch?v=UjkSFoLxesw
namespace Enemy
{
    public class EnemyAi : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform Player;
        public LayerMask whatIsGround, whatIsPlayer;

        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        public float timeBetweenAttacks;
        bool alreadyAttacked;

        public float sightRange, attackRange, meleeAttackRange, ForwardForce, UpwardForce;
        public bool playerInSightRange, playerInAttackRange, playerInMeleeAttackRange, meleeAttack,lookAtPlayer;
        public GameObject Bullet;
        private void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            playerInMeleeAttackRange = Physics.CheckSphere(transform.position, meleeAttackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange && !meleeAttack) AttackPlayer();
            else if (playerInSightRange && playerInMeleeAttackRange && meleeAttack) MeleeAttack();

        }

        private void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if(distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        private void SearchWalkPoint()
        { 
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
            walkPointSet = true;
        }
        
        private void ChasePlayer()
        {
            agent.SetDestination(Player.position);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            if (lookAtPlayer) transform.LookAt(Player);

            if(!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * ForwardForce, ForceMode.Impulse);
                rb.AddForce(transform.up * UpwardForce, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        private void MeleeAttack()
        {

        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}
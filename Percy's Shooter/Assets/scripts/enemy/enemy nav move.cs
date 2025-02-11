using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMove : MonoBehaviour
{
    [SerializeField]
    float chaseDistance = 10;
    [SerializeField]
    bool sendHome = true;
    Vector3 home;
    GameObject player;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        if (dir.magnitude < chaseDistance)
        {
            agent.destination = player.transform.position;
        }
        else if (sendHome == true)
        {
            agent.destination = home;
        }
    }
}
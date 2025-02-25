using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace player
{
    public class Ability3 : MonoBehaviour
    {
        public float test;
        public LayerMask whatIsPlayer;
        public bool Sphere;
        public SphereCollider SC;
        public List<GameObject> Enemy;
        // Start is called before the first frame update
        void Start()
        {
            Enemy = new List<GameObject>();
            SC = GetComponent<SphereCollider>();
        }


        void Update()
        {
            SC.radius = test;
            Sphere = Physics.CheckSphere(transform.position, test, whatIsPlayer);
            if (Input.GetKeyDown(KeyCode.V))
            {
                OnStay(SC);
            }
        }
        public void OnStay(Collider Coll)
        {
                if(Coll.CompareTag("Enemy"))
                {
                    Enemy.Add(Coll.gameObject);
                    Debug.Log(Enemy.Count);
                }
            //change this to a independent thing
            //set enemy move speed to a set amount
            //wait
            //set enemy speed back to normal
        }
    }
}
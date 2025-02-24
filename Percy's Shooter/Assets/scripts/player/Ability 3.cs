using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class Ability3 : MonoBehaviour
    {
        public float test;
        public LayerMask whatIsPlayer;
        public bool Sphere;
        public SphereCollider SC;
        // Start is called before the first frame update
        void Start()
        {
            SC = GetComponent<SphereCollider>();
        }


        void Update()
        {
            SC.radius = test;
            Sphere = Physics.CheckSphere(transform.position, test, whatIsPlayer);
            if (Sphere & Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnTriggerStay(SC);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            //change this to a independent thing
            //set enemy move speed to a set amount
            //wait
            //set enemy speed back to normal
        }
    }
}
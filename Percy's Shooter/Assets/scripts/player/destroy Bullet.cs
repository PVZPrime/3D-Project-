using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace player
{
    public class DestroyBullet : MonoBehaviour
    {
        public float timeToDestroy = 5f;
        public float Timer;
        public bool Active;
        void Update()
        {
            if (Active) Timer += Time.deltaTime;
            if (Timer <= timeToDestroy && gameObject.GetComponent<MeshRenderer>().enabled == true) Active = true;
            //add a if statment to activate the compenents if active
            if (Timer >= timeToDestroy)
            {
                //Destroy(gameObject);
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Active = false;

                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                //gameObject.SetActive(false);
                Timer = 0;
            }
        }
        private void OnTriggerEnter(Collider coll)
        {
            if (coll != CompareTag("Bullet")) /*Destroy(gameObject);*/gameObject.SetActive(false);
        }
    }
}

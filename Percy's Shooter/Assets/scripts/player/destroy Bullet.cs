using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class DestroyBullet : MonoBehaviour
    {
        public float timeToDestroy = 5f;
        public float Timer {  get; set; }
        public bool Active { get; set; }
        void Update()
        {
            Timer += Time.deltaTime;
            if (Timer <= timeToDestroy) Active = true;
            if (Timer >= timeToDestroy)
            {
                //Destroy(gameObject);
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Active = false;
                gameObject.SetActive(false);
                Timer = 0;
            }
        }
        private void OnTriggerEnter(Collider coll)
        {
            if (coll != CompareTag("Bullet")) /*Destroy(gameObject);*/gameObject.SetActive(false);
        }
    }
}

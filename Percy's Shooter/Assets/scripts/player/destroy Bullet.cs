using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class DestroyBullet : MonoBehaviour
    {
        public float timeToDestroy = 5f;
        float timer;
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeToDestroy)
            {
                //Destroy(gameObject);
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.SetActive(false);
                timer = 0;
            }
        }
        private void OnTriggerEnter(Collider coll)
        {
            if (coll != CompareTag("Bullet")) /*Destroy(gameObject);*/gameObject.SetActive(false);
        }
    }
}

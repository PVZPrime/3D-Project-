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
                Destroy(gameObject);
            }
        }
        private void OnTriggerEnter(Collider coll)
        {
            if (coll != CompareTag("Bullet")) Destroy(gameObject);
        }
    }
}

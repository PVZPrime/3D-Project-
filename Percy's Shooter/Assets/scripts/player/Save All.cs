using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace player
{
    public class SaveAll : MonoBehaviour
    {
        public List<GameObject> Enemy;
        public List<GameObject> Bullet;
        // Start is called before the first frame update
        void Start()
        {
            Enemy = new List<GameObject>();
            Bullet = new List<GameObject>();
        }


        public void Save()
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Enemy.Add(go);
                //go.GetComponent<SaveScript>().Save();
            }
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
            {
                Bullet.Add(go);
                //go.GetComponent<SaveScript>().Save();
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>().Save();
        }
    }
}
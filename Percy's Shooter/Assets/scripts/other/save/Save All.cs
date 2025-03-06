using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class SaveAll : MonoBehaviour
    {
        public List<GameObject> Enemy;
        //public List<GameObject> Bullet;
        // Start is called before the first frame update
        void Start()
        {
            Enemy = new List<GameObject>();
            //Bullet = new List<GameObject>();
            //foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
            //{
            //    Bullet.Add(go);
            //}
            GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>().Save();
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Enemy.Add(go);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Load();
            }
        }
        public void Save()
        {
            //foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
            //{
            //    //go.GetComponent<SaveBullet>().Save();
            //}
            foreach (GameObject go in Enemy)
            {
                var enemySave = go.GetComponent<EnemySave>();
                if (enemySave != null)
                {
                    enemySave.Save();
                }
                else
                {
                    Debug.LogWarning($"EnemySave component not found on {go.name}");
                }
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>().Save();
        }
        public void Load()
        {
            foreach (GameObject go in Enemy)
            {
                var enemySave = go.GetComponent<EnemySave>();
                if (enemySave != null)
                {
                    enemySave.Load();
                }
                else
                {
                    Debug.LogWarning($"EnemySave component not found on {go.name}");
                }
            }
            //foreach (GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
            //{
            //    //go.GetComponent<SaveBullet>().Load();
            //}
            GameObject.FindGameObjectWithTag("Player").GetComponent<SaveScript>().Load();
        }
    }
}
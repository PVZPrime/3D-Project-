using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHP : MonoBehaviour
    {
        [SerializeField]
        float health = 2;
        float MaxHealth;
        float time;
        [SerializeField]
        float ImunityTime = 0.25f;
        Image healthbar;
        //GameObject player;
        public int experianceAmount;
        public bool EnemyDead;
        void Start()
        {
            //player = GameObject.FindGameObjectWithTag("Player");
            MaxHealth = health;
            //healthbar = GetComponentsInChildren<Image>()[1];
            //healthbar.fillAmount = health / MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
        }
        public void TakeDamage(int damage)
        {
            if (time >= ImunityTime)
            {
                health -= damage;
                //healthbar.fillAmount = health / MaxHealth;
                if (health <= 0)
                {
                    //player.GetComponent<XpScript>().GiveXP(experianceAmount);
                    GetComponent<LootDropChance>().InstantiateLoot(transform.position);
                    time = 0;
                    gameObject.SetActive(false);
                    EnemyDead = true;
                }
                time = 0;
            }
        }
    }
}
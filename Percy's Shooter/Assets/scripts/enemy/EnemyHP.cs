using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHP : MonoBehaviour
    {
        public float health = 2;
        //float MaxHealth;
        float time;
        [SerializeField]
        float ImunityTime = 0.25f;
        //Image healthbar;
        //GameObject player;
        public int experianceAmount;
        public bool EnemyDead;
        CapsuleCollider CC;
        SphereCollider SC;
        NavMeshAgent NMA;
        EnemyAiMelee EAM;
        EnemyAi EA;

        void Start()
        {
            CC = gameObject.GetComponent<CapsuleCollider>();
            SC = gameObject.GetComponent<SphereCollider>();
            NMA = gameObject.GetComponent<NavMeshAgent>();
            EAM = gameObject.GetComponent<EnemyAiMelee>();
            EA = gameObject.GetComponent<EnemyAi>();
            
            //player = GameObject.FindGameObjectWithTag("Player");
            //MaxHealth = health;
            //healthbar = GetComponentsInChildren<Image>()[1];
            //healthbar.fillAmount = health / MaxHealth;
        }

        void Update()
        {
            time += Time.deltaTime;
            if (EnemyDead)
            {
                CC.enabled = false;
                if (SC != null) SC.enabled = false;
                NMA.enabled = false;
                if (EAM != null) EAM.enabled = false;
                if (EA != null) EA.enabled = false;
            }
            else
            {
                CC.enabled = true;
                if(SC != null) SC.enabled = true;
                NMA.enabled = true;
                if (EAM != null) EAM.enabled = false;
                if (EA != null) EA.enabled = false;
            }
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
                    CC.enabled = false;
                    if (SC != null) gameObject.GetComponent<SphereCollider>().enabled = false;
                    NMA.enabled = false;
                    if(EAM != null)EAM.enabled = false;
                    if (EA != null) EA.enabled = false;
                    EnemyDead = true;
                }
                time = 0;
            }
        }
    }
}
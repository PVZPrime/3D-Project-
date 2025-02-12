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
        GameObject player;
        public int experianceAmount;

/*        [Header("Item Chance values")]
        [Header("Item Chance Max Must be 1 greater than you want")]

        public int ItemDropAmount;
        public int itemChanceMax = 1;
        int itemChanceMin = 0;
        public int itemChance;

        [Header("Item range must equal item chance Max")]
        [Header("Item 1 random range")]
        public int item1Min;
        public int item1Max;

		[Header("Item 2 random range")]
        public int item2Min;
        public int item2Max;

        [Header("Item 3 random range")]
        public int item3Min;
        public int item3Max;

        [Header("No Drop random range")]
        public int NoDropMin;
        public int NoDropMax;*/
        // Start is called before the first frame update
        void Start()
        {
            //itemChance = Random.Range(itemChanceMin, itemChanceMax);
            player = GameObject.FindGameObjectWithTag("Player");
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
                    player.GetComponent<XpScript>().GiveXP(experianceAmount);
                    GetComponent<LootDropChance>().InstantiateLoot(transform.position);
                    time = 0;
                    //DropItems(ItemDropAmount);

                    Destroy(gameObject);
                }
                time = 0;
            }
        }
        public void DropItems(int items)
        {
            //itemChance = Random.Range(itemChanceMin, itemChanceMax);
            
        }
    }
}
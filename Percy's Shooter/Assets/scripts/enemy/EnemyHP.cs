using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    float health = 2;
    float MaxHealth;
    float time;
    [SerializeField]
    float ImunityTime = 0.25f;
    Image healthbar;
    public int ItemDropAmount;
    GameObject player;
    public int experianceAmount;
    // Start is called before the first frame update
    void Start()
    {
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
                DropItems(ItemDropAmount);
                Destroy(gameObject);
            }
            time = 0;
        }
    }
    public void DropItems(int items)
    { 
    
    }
}

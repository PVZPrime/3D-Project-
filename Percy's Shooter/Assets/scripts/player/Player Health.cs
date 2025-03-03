using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health = 2;
        float MaxHealth;
        float time;
        float timer;
        public float ImunityTime = 0.25f;
        public Image healthbar;
        public float RegenAmount;
        public float RegenDelay;
        // Start is called before the first frame update
        void Start()
        {
            MaxHealth = health;
            if(healthbar != null ) healthbar.fillAmount = health / MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            timer = Time.deltaTime;
            if(timer >= RegenDelay)
            {
                health += RegenAmount;
            }
            if (health > MaxHealth)
            {
                health = MaxHealth;
            }
        }
        public void TakeDamage(int damage)
        {
            if (time >= ImunityTime)
            {
                health -= damage;
                if (healthbar != null) healthbar.fillAmount = health / MaxHealth;
                if (health <= 0)
                {
                    
                }
                time = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        float health = 2;
        float MaxHealth;
        float time;
        [SerializeField]
        float ImunityTime = 0.25f;
        public Image healthbar;
        // Start is called before the first frame update
        void Start()
        {
            MaxHealth = health;
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

                }
                time = 0;
            }
        }
    }
}

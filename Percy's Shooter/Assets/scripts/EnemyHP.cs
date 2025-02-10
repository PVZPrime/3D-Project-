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
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        healthbar = GetComponentsInChildren<Image>()[1];
        healthbar.fillAmount = health / MaxHealth;
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
            healthbar.fillAmount = health / MaxHealth;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            time = 0;
        }
    }
}

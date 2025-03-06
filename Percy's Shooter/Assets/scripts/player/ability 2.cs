using NewMovment;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

namespace player
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    //String.Format("{0:0.00}", value);
    //use this to show cooldown
    public class ability2 : MonoBehaviour
    {
        public float PlayerSpeedBuffed;
        float BasePlayerSpeed;
        public float PlayerSprintSpeedBuffed;
        float BasePlayerSprintSpeed;
        public float PlayerRegenAmountBuffed;
        float BasePlayerRegenAmount;
        private StarterAssetsInputs _input;
        private PlayerHealth PlayerHealth;
        private PlayerMovement PM;
        Animator anim;
        public TextMeshProUGUI CoolDownCounter;
        public float Cooldown;
        public float Length { get; set; }
        public float time { get; set; }
        bool abilityActivated;
        bool trigger;
        // Start is called before the first frame update
        void Start()
        {
            anim = GameObject.FindGameObjectWithTag("left").GetComponent<Animator>();
            _input = GetComponent<StarterAssetsInputs>();
            PM = GetComponent<PlayerMovement>();
            PlayerHealth = GetComponent<PlayerHealth>();
            BasePlayerSpeed = PM.walkSpeed;
            BasePlayerSprintSpeed = PM.sprintSpeed;
            BasePlayerRegenAmount = PlayerHealth.RegenAmount;
            Length = Cooldown / 2;
            time = Cooldown;
        }

        // Update is called once per frame
        void Update()
        {
            CoolDownCounter.SetText(String.Format("{0:0.00}", time));
            if (abilityActivated)
            {
                time -= Time.deltaTime;
                Length -= Time.deltaTime;
            }
            if(_input.Ability2 && time >= 0)
            {
                abilityActivated = true;
                if (Length > 0)
                {
                    if (anim != null && !trigger)
                    {
                        trigger = true;
                        anim.SetTrigger("ability2");
                    }
                    PM.walkSpeed = PlayerSpeedBuffed;
                    PM.sprintSpeed = PlayerSprintSpeedBuffed;
                    PlayerHealth.RegenAmount = PlayerRegenAmountBuffed;
                }
            }
            else if(time <= 0)
            {
                time = Cooldown;
                Length = Cooldown / 2;
                abilityActivated = false;
            }
                if (Length <= 0)
                {
                    PM.walkSpeed = BasePlayerSpeed;
                    PM.sprintSpeed = BasePlayerSprintSpeed;
                    PlayerHealth.RegenAmount = BasePlayerRegenAmount;
                    trigger = false;
                }
        }
    }
}
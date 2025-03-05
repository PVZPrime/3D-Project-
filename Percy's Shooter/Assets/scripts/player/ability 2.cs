using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace player
{
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif

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
        private FirstPersonController FPC;
        Animator anim;
        bool AbilityActive;
        public float Cooldown;
        public float Length { get; set; }
        public float time { get; set; }
        bool abilityActivated;
        // Start is called before the first frame update
        void Start()
        {
            anim = GameObject.FindGameObjectWithTag("left").GetComponent<Animator>();
            _input = GetComponent<StarterAssetsInputs>();
            FPC = GetComponent<FirstPersonController>();
            PlayerHealth = GetComponent<PlayerHealth>();
            BasePlayerSpeed = FPC.MoveSpeed;
            BasePlayerSprintSpeed = FPC.SprintSpeed;
            BasePlayerRegenAmount = PlayerHealth.RegenAmount;
            Length = Cooldown / 2;
            time = Cooldown;
        }

        // Update is called once per frame
        void Update()
        {
            if (abilityActivated)
            {
                time -= Time.deltaTime;
                Length -= Time.deltaTime;
            }
            AbilityActive = _input.Ability2;
            if(_input.Ability2 && time >= 0)
            {
                if(anim != null)    anim.SetTrigger("ability2");
                abilityActivated = true;
                if (Length > 0)
                {
                    FPC.MoveSpeed = PlayerSpeedBuffed;
                    FPC.SprintSpeed = PlayerSprintSpeedBuffed;
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
                    FPC.MoveSpeed = BasePlayerSpeed;
                    FPC.SprintSpeed = BasePlayerSprintSpeed;
                    PlayerHealth.RegenAmount = BasePlayerRegenAmount;
                }
        }
    }
}
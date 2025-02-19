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
        bool AbilityActive;
        public float Cooldown;
        public float Length;
        float BaseLength;
        public float time;
        // Start is called before the first frame update
        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
            FPC = GetComponent<FirstPersonController>();
            PlayerHealth = GetComponent<PlayerHealth>();
            BasePlayerSpeed = FPC.MoveSpeed;
            BasePlayerSprintSpeed = FPC.SprintSpeed;
            BasePlayerRegenAmount = PlayerHealth.RegenAmount;
            Length = Cooldown / 2;
            BaseLength = Cooldown / 2;
            time = Cooldown;
        }

        // Update is called once per frame
        void Update()
        {
            AbilityActive = _input.Ability2;
            if(_input.Ability2 && time >= 0)
            {
                time -= Time.deltaTime;
                Length -= Time.deltaTime;
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
                Length = BaseLength;
            }
                if (Length <= 0)
                {
                    BasePlayerSpeed = FPC.MoveSpeed;
                    BasePlayerSprintSpeed = FPC.SprintSpeed;
                    BasePlayerRegenAmount = PlayerHealth.RegenAmount;
                }
        }
    }
}